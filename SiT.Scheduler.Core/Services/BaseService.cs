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
    private readonly IRepository<TEntity> _repository;
    private readonly IEntityValidatorFactory _entityValidatorFactory;
    private readonly IDataTransformerFactory _dataTransformerFactory;

    protected BaseService(IRepository<TEntity> repository, IEntityValidatorFactory entityValidatorFactory, IDataTransformerFactory dataTransformerFactory)
    {
        this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this._entityValidatorFactory = entityValidatorFactory ?? throw new ArgumentNullException(nameof(entityValidatorFactory));
        this._dataTransformerFactory = dataTransformerFactory ?? throw new ArgumentNullException(nameof(dataTransformerFactory));
    }

    public Task<IOperationResult<TLayout>> GetAsync<TLayout>(TExternalRequirement externalRequirement, Guid id, CancellationToken cancellationToken, IQueryEntityOptions<TEntity> options = null)
        => this.GetInternallyAsync(
            externalRequirement,
            id,
            allFilters =>
            {
                var operationResult = new OperationResult<TLayout>();

                var getProjection = this.GetProjection<TLayout>();
                if (!getProjection.IsSuccessful) return Task.FromResult<IOperationResult<TLayout>>(operationResult.AppendErrors(getProjection));

                return this._repository.GetAsync(allFilters, getProjection.Data, cancellationToken);
            },
            options);

    public Task<IOperationResult<TEntity>> GetAsync(TExternalRequirement externalRequirement, Guid id, CancellationToken cancellationToken, IQueryEntityOptions<TEntity> options = null)
        => this.GetInternallyAsync(externalRequirement, id, allFilters => this._repository.GetAsync(allFilters, cancellationToken), options);


    public Task<IOperationResult<IEnumerable<TLayout>>> GetManyAsync<TLayout>(TExternalRequirement externalRequirement, CancellationToken cancellationToken, IQueryEntityOptions<TEntity> options = null)
        => this.GetManyInternallyAsync(
            externalRequirement,
            allFilters =>
            {
                var operationResult = new OperationResult<IEnumerable<TLayout>>();

                var getProjection = this.GetProjection<TLayout>();
                if (!getProjection.IsSuccessful) return Task.FromResult<IOperationResult<IEnumerable<TLayout>>>(operationResult.AppendErrors(getProjection));

                return this._repository.GetManyAsync(allFilters, getProjection.Data, cancellationToken);
            },
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

    protected abstract Expression<Func<TEntity, bool>> ConstructFilter(TExternalRequirement externalRequirement);
    protected abstract TEntity InitializeEntity([NotNull] TPrototype prototype);
    protected abstract Task<IOperationResult> ApplyPrototypeAsync([NotNull] TPrototype prototype, [NotNull] TEntity entity, CancellationToken cancellationToken);

    private static Expression<Func<TEntity, bool>> ConstructIdFilter(Guid id) => x => x.Id == id;

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

    private async Task<IOperationResult<TResult>> GetInternallyAsync<TResult>(
        TExternalRequirement externalRequirement,
        Guid id,
        Func<IEnumerable<Expression<Func<TEntity, bool>>>, Task<IOperationResult<TResult>>> getEntities,
        IQueryEntityOptions<TEntity> options = null)
    {
        var operationResult = new OperationResult<TResult>();
        operationResult.ValidateNotNull(externalRequirement);
        operationResult.ValidateNotDefault(id);
        if (!operationResult.IsSuccessful) return operationResult;

        var idFilter = ConstructIdFilter(id);
        var externalRequirementFilter = this.ConstructFilter(externalRequirement);
        var optionsFilters = (options?.AdditionalFilters).OrEmptyIfNull().IgnoreNullValues();

        var allFilters = new List<Expression<Func<TEntity, bool>>> { idFilter, externalRequirementFilter };
        allFilters.AddRange(optionsFilters);

        var getResult = await getEntities(allFilters);
        if (!getResult.IsSuccessful) return operationResult.AppendErrors(getResult);

        operationResult.Data = getResult.Data;
        return operationResult;
    }

    private async Task<IOperationResult<IEnumerable<TResult>>> GetManyInternallyAsync<TResult>(
        TExternalRequirement externalRequirement,
        Func<IEnumerable<Expression<Func<TEntity, bool>>>, Task<IOperationResult<IEnumerable<TResult>>>> getEntities,
        IQueryEntityOptions<TEntity> options = null)
    {
        var operationResult = new OperationResult<IEnumerable<TResult>>();
        operationResult.ValidateNotNull(externalRequirement);
        if (!operationResult.IsSuccessful) return operationResult;

        var externalRequirementFilter = this.ConstructFilter(externalRequirement);
        var optionsFilters = (options?.AdditionalFilters).OrEmptyIfNull().IgnoreNullValues();

        var allFilters = new List<Expression<Func<TEntity, bool>>> { externalRequirementFilter };
        allFilters.AddRange(optionsFilters);

        var getResult = await getEntities(allFilters);
        if (!getResult.IsSuccessful) return operationResult.AppendErrors(getResult);

        operationResult.Data = getResult.Data.OrEmptyIfNull();
        return operationResult;
    }
}