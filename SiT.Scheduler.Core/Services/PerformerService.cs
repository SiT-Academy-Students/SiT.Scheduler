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

public class PerformerService : BaseService<Performer, IDefaultExternalRequirement, IPerformerPrototype>, IPerformerService
{
    public PerformerService(IRepository<Performer> repository, IEntityValidatorFactory entityValidatorFactory, IDataTransformerFactory dataTransformerFactory) 
        : base(repository, entityValidatorFactory, dataTransformerFactory)
    {
    }

    protected override Performer InitializeEntity(IPerformerPrototype prototype) => new ();

    protected override Task<IOperationResult> ApplyPrototypeAsync(IPerformerPrototype prototype, Performer entity, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult();
        entity.Name = prototype.Name;

        return Task.FromResult<IOperationResult>(operationResult);
    }

    protected override OperationResult<Expression<Func<Performer, bool>>> ConstructFilter(IDefaultExternalRequirement externalRequirement) => new();
}
