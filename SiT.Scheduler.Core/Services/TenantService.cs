namespace SiT.Scheduler.Core.Services;

using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using SiT.Scheduler.Core.Contracts.OperativeModels.ExternalRequirements;
using SiT.Scheduler.Core.Contracts.OperativeModels.Prototypes;
using SiT.Scheduler.Core.Contracts.Services;
using SiT.Scheduler.Core.Contracts.Transformations;
using SiT.Scheduler.Data.Contracts.Repositories;
using SiT.Scheduler.Data.Models;
using SiT.Scheduler.Utilities.OperationResults;
using SiT.Scheduler.Validation.Contracts;

public class TenantService : BaseService<Tenant, IDefaultExternalRequirement, ITenantPrototype>, ITenantService
{
    public TenantService(IRepository<Tenant> repository, IEntityValidatorFactory entityValidatorFactory, IDataTransformerFactory dataTransformerFactory) : base(repository, entityValidatorFactory, dataTransformerFactory)
    {
    }

    protected override Expression<Func<Tenant, bool>> ConstructFilter(IDefaultExternalRequirement externalRequirement) => null;

    protected override Tenant InitializeEntity(ITenantPrototype prototype) => new();

    protected override Task<IOperationResult> ApplyPrototypeAsync(ITenantPrototype prototype, Tenant entity, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult();

        entity.Name = prototype.Name;
        entity.IsSystem = prototype.IsSystem;
        return Task.FromResult<IOperationResult>(operationResult);
    }
}