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

public class IdentityService : BaseService<Identity, IDefaultExternalRequirement, IIdentityPrototype>, IIdentityService
{
    public IdentityService(IRepository<Identity> repository, IEntityValidatorFactory entityValidatorFactory, IDataTransformerFactory dataTransformerFactory) : base(repository, entityValidatorFactory, dataTransformerFactory)
    {
    }

    protected override Expression<Func<Identity, bool>> ConstructFilter(IDefaultExternalRequirement externalRequirement) => null;

    protected override Identity InitializeEntity(IIdentityPrototype prototype) => new();

    protected override Task<IOperationResult> ApplyPrototypeAsync(IIdentityPrototype prototype, Identity entity, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult();

        entity.Id = prototype.Id;
        entity.DisplayName = prototype.DisplayName;

        return Task.FromResult<IOperationResult>(operationResult);
    }
}