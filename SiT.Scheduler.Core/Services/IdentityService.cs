namespace SiT.Scheduler.Core.Services;

using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using SiT.Scheduler.Core.Contracts.OperativeModels.ExternalRequirements;
using SiT.Scheduler.Core.Contracts.OperativeModels.Prototypes;
using SiT.Scheduler.Core.Contracts.Services;
using SiT.Scheduler.Core.Contracts.Transformations;
using SiT.Scheduler.Core.OperativeModels.ExternalRequirements;
using SiT.Scheduler.Data.Contracts.Repositories;
using SiT.Scheduler.Data.Models;
using SiT.Scheduler.Utilities.OperationResults;
using SiT.Scheduler.Validation.Contracts;

public class IdentityService : BaseService<Identity, IDefaultExternalRequirement, IIdentityPrototype>, IIdentityService
{
    private readonly ITenantService _tenantService;

    public IdentityService(IRepository<Identity> repository, IEntityValidatorFactory entityValidatorFactory, IDataTransformerFactory dataTransformerFactory, ITenantService tenantService) : base(repository, entityValidatorFactory, dataTransformerFactory)
    {
        this._tenantService = tenantService ?? throw new ArgumentNullException(nameof(tenantService));
    }

    protected override OperationResult<Expression<Func<Identity, bool>>> ConstructFilter(IDefaultExternalRequirement externalRequirement) => new();

    protected override Identity InitializeEntity(IIdentityPrototype prototype) => new();

    protected override async Task<IOperationResult> ApplyPrototypeAsync(IIdentityPrototype prototype, Identity entity, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult();

        var getTenant = await this._tenantService.GetAsync(ExternalRequirement.Default, prototype.TenantId, cancellationToken);
        if (!getTenant.IsSuccessful) return operationResult.AppendErrors(getTenant);

        var tenant = getTenant.Data;
        operationResult.ValidateNotNull(tenant);
        if (!operationResult.IsSuccessful) return operationResult;

        entity.Id = prototype.Id;
        entity.DisplayName = prototype.DisplayName;
        entity.Tenants.Add(tenant);

        return operationResult;
    }
}