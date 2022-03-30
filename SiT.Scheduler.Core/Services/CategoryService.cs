namespace SiT.Scheduler.Core.Services;

using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using SiT.Scheduler.Core.Contracts.Authentication;
using SiT.Scheduler.Core.Contracts.OperativeModels.ExternalRequirements;
using SiT.Scheduler.Core.Contracts.OperativeModels.Prototypes;
using SiT.Scheduler.Core.Contracts.Services;
using SiT.Scheduler.Core.Contracts.Transformations;
using SiT.Scheduler.Core.Extensions;
using SiT.Scheduler.Data.Contracts.Repositories;
using SiT.Scheduler.Data.Models;
using SiT.Scheduler.Utilities.OperationResults;
using SiT.Scheduler.Validation.Contracts;

public class CategoryService : BaseService<Category, IDefaultExternalRequirement, ICategoryPrototype>, ICategoryService
{
    private readonly ITenantContext _tenantContext;

    public CategoryService(IRepository<Category> repository, IEntityValidatorFactory entityValidatorFactory, IDataTransformerFactory dataTransformerFactory, ITenantContext tenantContext)
        : base(repository, entityValidatorFactory, dataTransformerFactory)
    {
        this._tenantContext = tenantContext ?? throw new ArgumentNullException(nameof(tenantContext));
    }

    protected override Category InitializeEntity(ICategoryPrototype prototype) => new();

    protected override Task<IOperationResult> ApplyPrototypeAsync(ICategoryPrototype prototype, Category entity, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult();
        operationResult.ValidateTenantContext(this._tenantContext);
        if (!operationResult.IsSuccessful) return Task.FromResult<IOperationResult>(operationResult);

        entity.Name = prototype.Name;
        entity.Description = prototype.Description;
        entity.TenantId = this._tenantContext.Tenant.Id;

        return Task.FromResult<IOperationResult>(operationResult);
    }

    protected override OperationResult<Expression<Func<Category, bool>>> ConstructFilter(IDefaultExternalRequirement externalRequirement)
    {
        var operationResult = new OperationResult<Expression<Func<Category, bool>>>();
        operationResult.ValidateTenantContext(this._tenantContext);
        if (!operationResult.IsSuccessful) return operationResult;

        operationResult.Data = c => c.TenantId == this._tenantContext.Tenant.Id;
        return operationResult;
    }
}