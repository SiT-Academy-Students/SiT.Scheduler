namespace SiT.Scheduler.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using SiT.Scheduler.Core.Contracts.OperativeModels.ExternalRequirements;
using SiT.Scheduler.Core.Contracts.OperativeModels.Prototypes;
using SiT.Scheduler.Core.Contracts.Services;
using SiT.Scheduler.Core.Contracts.Transformations;
using SiT.Scheduler.Data.Contracts.Repositories;
using SiT.Scheduler.Data.Models;
using SiT.Scheduler.Validation.Contracts;

public class PerformerService : BaseService<Performer, IDefaultExternalRequirement, IPerformerPrototype>, IPerformerService
{
    public PerformerService(IRepository<Performer> repository, IEntityValidatorFactory entityValidatorFactory, IDataTransformerFactory dataTransformerFactory) : base(repository, entityValidatorFactory, dataTransformerFactory)
    {
    }

    protected override Performer InitializeEntity([NotNull] IPerformerPrototype prototype) => new ();
    
    protected override void ApplyPrototype([NotNull] IPerformerPrototype prototype, [NotNull] Performer entity)
    {
        entity.Name = prototype.Name;
    }

    protected override Expression<Func<Performer, bool>> ConstructFilter(IDefaultExternalRequirement externalRequirement) => null;
}
