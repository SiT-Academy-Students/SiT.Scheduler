namespace SiT.Scheduler.Core.Services;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using SiT.Scheduler.Core.Contracts.OperativeModels;
using SiT.Scheduler.Core.Contracts.Options;
using SiT.Scheduler.Core.Contracts.Services;
using SiT.Scheduler.Core.Contracts.Transformations;
using SiT.Scheduler.Core.OperativeModels;
using SiT.Scheduler.Data.Contracts.Models;
using SiT.Scheduler.Data.Contracts.Repositories;
using SiT.Scheduler.Utilities;
using SiT.Scheduler.Utilities.Errors;
using SiT.Scheduler.Utilities.OperationResults;
using SiT.Scheduler.Validation.Contracts;

public abstract class BaseService<TEntity, TExternalRequirement, TPrototype> : IService<TEntity, TExternalRequirement, TPrototype>
    where TEntity : class, IEntity
    where TExternalRequirement : class
    where TPrototype : class
{
    protected delegate Task<IOperationResult<TResult>> GetEntityFunc<TResult>(IEnumerable<Expression<Func<TEntity, bool>>> filters);
    protected delegate Task<IOperationResult<IEnumerable<TResult>>> GetEntitiesFunc<TResult>(IEnumerable<Expression<Func<TEntity, bool>>> filters);

    private readonly IRepository<TEntity> _repository;
    private readonly IEntityValidatorFactory _entityValidatorFactory;
    private readonly IDataTransformerFactory _dataTransformerFactory;

    protected BaseService(IRepository<TEntity> repository, IEntityValidatorFactory entityValidatorFactory, IDataTransformerFactory dataTransformerFactory)
    {
        this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this._entityValidatorFactory = entityValidatorFactory ?? throw new ArgumentNullException(nameof(entityValidatorFactory));
        this._dataTransformerFactory = dataTransformerFactory ?? throw new ArgumentNullException(nameof(dataTransformerFactory));
    }

    public async Task<IOperationResult<bool>> AnyAsync(TExternalRequirement externalRequirement, CancellationToken cancellationToken, IQueryEntityOptions<TEntity> options = null)
    {
        var operationResult = new OperationResult<bool>();
        operationResult.ValidateNotNull(externalRequirement);
        if (!operationResult.IsSuccessful) return operationResult;

        var constructStandardFilters = this.ConstructStandardFilters(externalRequirement, options);
        if (!constructStandardFilters.IsSuccessful) return operationResult.AppendErrors(constructStandardFilters);

        var allFilters = constructStandardFilters.Data;
        var anyEntityMatches = await this._repository.AnyAsync(allFilters, cancellationToken);
        if (!anyEntityMatches.IsSuccessful) return operationResult.AppendErrors(anyEntityMatches);

        operationResult.Data = anyEntityMatches.Data;
        return operationResult;
    }

    public Task<IOperationResult<TLayout>> GetAsync<TLayout>(TExternalRequirement externalRequirement, Guid id, CancellationToken cancellationToken, IQueryEntityOptions<TEntity> options = null)
    {
        var operationResult = new OperationResult<TLayout>();
        operationResult.ValidateNotNull(externalRequirement);
        operationResult.ValidateNotDefault(id);
        if (!operationResult.IsSuccessful) return Task.FromResult<IOperationResult<TLayout>>(operationResult);

        var idFilter = ConstructIdFilter(id);

        return this.GetInternallyAsync<TLayout>(externalRequirement, idFilter.AsEnumerable(), cancellationToken, options);
    }

    public Task<IOperationResult<TEntity>> GetAsync(TExternalRequirement externalRequirement, Guid id, CancellationToken cancellationToken, IQueryEntityOptions<TEntity> options = null)
    {
        var operationResult = new OperationResult<TEntity>();
        operationResult.ValidateNotNull(externalRequirement);
        operationResult.ValidateNotDefault(id);
        if (!operationResult.IsSuccessful) return Task.FromResult<IOperationResult<TEntity>>(operationResult);

        var idFilter = ConstructIdFilter(id);
        return this.GetInternallyAsync(externalRequirement, idFilter.AsEnumerable(), allFilters => this._repository.GetAsync(allFilters, cancellationToken), options);
    }

    public Task<IOperationResult<IEnumerable<TLayout>>> GetManyAsync<TLayout>(TExternalRequirement externalRequirement, CancellationToken cancellationToken, IQueryEntityOptions<TEntity> options = null)
        => this.GetManyInternallyAsync(
            externalRequirement,
            filters => this.GetTransformedEntitiesAsync<TLayout>(filters ,cancellationToken),
            options);

    public Task<IOperationResult<IEnumerable<TEntity>>> GetManyAsync(TExternalRequirement externalRequirement, CancellationToken cancellationToken, IQueryEntityOptions<TEntity> options = null)
        => this.GetManyInternallyAsync(externalRequirement, allFilters => this._repository.GetManyAsync(allFilters, cancellationToken), options);

    public async Task<IOperationResult<ICreateEntityResult>> CreateAsync(TPrototype prototype, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<ICreateEntityResult>();
        operationResult.ValidateNotNull(prototype);
        if (!operationResult.IsSuccessful) return operationResult;

        var prepareEntity = await this.PrepareEntityAsync(prototype, () => this.InitializeEntity(prototype), cancellationToken);
        if (!prepareEntity.IsSuccessful) return operationResult.AppendErrors(prepareEntity);

        var initializedEntity = prepareEntity.Data;
        var createEntity = await this._repository.CreateAsync(initializedEntity, cancellationToken);
        if (!createEntity.IsSuccessful) return operationResult.AppendErrors(createEntity);

        operationResult.Data = new CreateEntityResult(initializedEntity.Id);
        return operationResult;
    }

    public async Task<IOperationResult> UpdateAsync(TExternalRequirement externalRequirement, Guid id, TPrototype prototype, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult();
        operationResult.ValidateNotNull(externalRequirement);
        operationResult.ValidateNotDefault(id);
        operationResult.ValidateNotNull(prototype);

        if (!operationResult.IsSuccessful) return operationResult;

        var getExistingEntity = await this.GetAsync(externalRequirement, id, cancellationToken);
        if (!getExistingEntity.IsSuccessful) return operationResult.AppendErrors(getExistingEntity);

        var existingEntity = getExistingEntity.Data;
        operationResult.ValidateNotNull(existingEntity, new EntityDoesNotExistError());
        if (!operationResult.IsSuccessful) return operationResult;

        var prepareEntity = await this.PrepareEntityAsync(prototype, () => existingEntity, cancellationToken);
        if (!prepareEntity.IsSuccessful) return operationResult.AppendErrors(prepareEntity);

        var updateEntity = await this._repository.UpdateAsync(existingEntity, cancellationToken);
        if (!updateEntity.IsSuccessful) return operationResult.AppendErrors(updateEntity);

        return operationResult;
    }

    protected Task<IOperationResult<TLayout>> GetInternallyAsync<TLayout>(TExternalRequirement externalRequirement, IEnumerable<Expression<Func<TEntity, bool>>> customFilters, CancellationToken cancellationToken, IQueryEntityOptions<TEntity> options = null)
    {
        var operationResult = new OperationResult<TLayout>();
        operationResult.ValidateNotNull(externalRequirement);
        if (!operationResult.IsSuccessful) return Task.FromResult<IOperationResult<TLayout>>(operationResult);

        return this.GetInternallyAsync(externalRequirement, customFilters, (filters) => this.GetTransformedEntityAsync<TLayout>(filters, cancellationToken), options);
    }

    protected abstract OperationResult<Expression<Func<TEntity, bool>>> ConstructFilter(TExternalRequirement externalRequirement);
    protected abstract TEntity InitializeEntity([NotNull] TPrototype prototype);
    protected abstract Task<IOperationResult> ApplyPrototypeAsync([NotNull] TPrototype prototype, [NotNull] TEntity entity, CancellationToken cancellationToken);

    private static Expression<Func<TEntity, bool>> ConstructIdFilter(Guid id) => x => x.Id == id;

    private Task<IOperationResult<TResult>> GetTransformedEntityAsync<TResult>(IEnumerable<Expression<Func<TEntity, bool>>> filters, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<TResult>();

        var getProjection = this.GetProjection<TResult>();
        if (!getProjection.IsSuccessful) return Task.FromResult<IOperationResult<TResult>>(operationResult.AppendErrors(getProjection));

        return this._repository.GetAsync(filters, getProjection.Data, cancellationToken);
    }

    private Task<IOperationResult<IEnumerable<TResult>>> GetTransformedEntitiesAsync<TResult>(IEnumerable<Expression<Func<TEntity, bool>>> filters, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<IEnumerable<TResult>>();

        var getProjection = this.GetProjection<TResult>();
        if (!getProjection.IsSuccessful) return Task.FromResult<IOperationResult<IEnumerable<TResult>>>(operationResult.AppendErrors(getProjection));

        return this._repository.GetManyAsync(filters, getProjection.Data, cancellationToken);
    }

    private async Task<IOperationResult<TEntity>> PrepareEntityAsync(TPrototype prototype, Func<TEntity> getEntity, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<TEntity>();

        operationResult.ValidateNotNull(prototype);
        operationResult.ValidateNotNull(getEntity);
        if (!operationResult.IsSuccessful) return operationResult;

        var prototypeValidator = this._entityValidatorFactory.BuildValidator<TPrototype>();
        var validatePrototype = await prototypeValidator.ValidateAsync(prototype, cancellationToken);
        if (!validatePrototype.IsSuccessful) return operationResult.AppendErrors(validatePrototype);

        var entity = getEntity();
        operationResult.ValidateNotNull(entity);
        if (!operationResult.IsSuccessful) return operationResult;

        var applyPrototype = await this.ApplyPrototypeAsync(prototype, entity, cancellationToken);
        if (!applyPrototype.IsSuccessful) return operationResult.AppendErrors(applyPrototype);

        var entityValidator = this._entityValidatorFactory.BuildValidator<TEntity>();
        var validateEntity = await entityValidator.ValidateAsync(entity, cancellationToken);
        if (!validateEntity.IsSuccessful) return operationResult.AppendErrors(validateEntity);

        operationResult.Data = entity;
        return operationResult;
    }

    private IOperationResult<Expression<Func<TEntity, TLayout>>> GetProjection<TLayout>()
    {
        var operationResult = new OperationResult<Expression<Func<TEntity, TLayout>>>();

        var dataTransformer = this._dataTransformerFactory.ConstructTransformer<TEntity, TLayout>();
        operationResult.ValidateNotNull(dataTransformer, new UnsupportedLayoutError());
        if (!operationResult.IsSuccessful) return operationResult;

        operationResult.Data = dataTransformer.Projection;
        return operationResult;
    }

    private async Task<IOperationResult<TResult>> GetInternallyAsync<TResult>(TExternalRequirement externalRequirement, IEnumerable<Expression<Func<TEntity, bool>>> customFilters, GetEntityFunc<TResult> getEntities, IQueryEntityOptions<TEntity> options = null)
    {
        var operationResult = new OperationResult<TResult>();
        operationResult.ValidateNotNull(externalRequirement);
        if (!operationResult.IsSuccessful) return operationResult;

        var constructStandardFilters = this.ConstructStandardFilters(externalRequirement, options);
        if (!constructStandardFilters.IsSuccessful) return operationResult.AppendErrors(constructStandardFilters);

        var allFilters = constructStandardFilters.Data.ConcatenateWith(customFilters).OrEmptyIfNull().IgnoreNullValues();

        var getResult = await getEntities(allFilters);
        if (!getResult.IsSuccessful) return operationResult.AppendErrors(getResult);

        operationResult.Data = getResult.Data;
        return operationResult;
    }

    private async Task<IOperationResult<IEnumerable<TResult>>> GetManyInternallyAsync<TResult>(TExternalRequirement externalRequirement, GetEntitiesFunc<TResult> getEntities, IQueryEntityOptions<TEntity> options = null)
    {
        var operationResult = new OperationResult<IEnumerable<TResult>>();
        operationResult.ValidateNotNull(externalRequirement);
        if (!operationResult.IsSuccessful) return operationResult;

        var constructStandardFilters = this.ConstructStandardFilters(externalRequirement, options);
        if (!constructStandardFilters.IsSuccessful) return operationResult.AppendErrors(constructStandardFilters);

        var allFilters = constructStandardFilters.Data;
        var getResult = await getEntities(allFilters);
        if (!getResult.IsSuccessful) return operationResult.AppendErrors(getResult);

        operationResult.Data = getResult.Data.OrEmptyIfNull();
        return operationResult;
    }

    private IOperationResult<IEnumerable<Expression<Func<TEntity, bool>>>> ConstructStandardFilters(TExternalRequirement externalRequirement, IQueryEntityOptions<TEntity> options)
    {
        var operationResult = new OperationResult<IEnumerable<Expression<Func<TEntity, bool>>>>();
        operationResult.ValidateNotNull(externalRequirement);
        if (!operationResult.IsSuccessful) return operationResult;

        var constructExternalRequirementFilter = this.ConstructFilter(externalRequirement);
        if (!constructExternalRequirementFilter.IsSuccessful) return operationResult.AppendErrors(constructExternalRequirementFilter);

        var externalRequirementFilter = constructExternalRequirementFilter.Data;
        var optionsFilters = (options?.AdditionalFilters).OrEmptyIfNull().IgnoreNullValues();

        operationResult.Data = externalRequirementFilter.ConcatenateWith(optionsFilters);
        return operationResult;
    }
}