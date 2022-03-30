namespace SiT.Scheduler.Core.Services;

using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using SiT.Scheduler.Core.Contracts.Authentication;
using SiT.Scheduler.Core.Contracts.Transformations;
using SiT.Scheduler.Core.Extensions;
using SiT.Scheduler.Data.Contracts.Models;
using SiT.Scheduler.Data.Contracts.Repositories;
using SiT.Scheduler.Utilities.OperationResults;
using SiT.Scheduler.Validation.Contracts;

public abstract class BaseTenantEntityService<TEntity, TExternalRequirement, TPrototype> : BaseService<TEntity, TExternalRequirement, TPrototype>
    where TEntity : class, IEntity, ITenantEntity
    where TExternalRequirement : class
    where TPrototype : class
{
    private readonly ITenantContext _tenantContext;

    protected BaseTenantEntityService(IRepository<TEntity> repository, IEntityValidatorFactory entityValidatorFactory, IDataTransformerFactory dataTransformerFactory, [NotNull] ITenantContext tenantContext)
        : base(repository, entityValidatorFactory, dataTransformerFactory)
    {
        this._tenantContext = tenantContext ?? throw new ArgumentNullException(nameof(tenantContext));
    }

    protected override OperationResult<Expression<Func<TEntity, bool>>> ConstructFilter(TExternalRequirement externalRequirement)
    {
        var operationResult = new OperationResult<Expression<Func<TEntity, bool>>>();
        operationResult.ValidateTenantContext(this._tenantContext);
        if (!operationResult.IsSuccessful) return operationResult;

        operationResult.Data = c => c.TenantId == this._tenantContext.Tenant.Id;
        return operationResult;
    }

    protected override Task<IOperationResult> ApplyPrototypeAsync(TPrototype prototype, TEntity entity, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult();
        operationResult.ValidateTenantContext(this._tenantContext);
        if (!operationResult.IsSuccessful) return Task.FromResult<IOperationResult>(operationResult);

        entity.TenantId = this._tenantContext.Tenant.Id;
        return Task.FromResult<IOperationResult>(operationResult);
    }
}