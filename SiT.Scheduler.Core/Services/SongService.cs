namespace SiT.Scheduler.Core.Services;
using System;
using System.Linq.Expressions;
using SiT.Scheduler.Core.Contracts.OperativeModels.ExternalRequirements;
using SiT.Scheduler.Core.Contracts.OperativeModels.Prototypes;
using SiT.Scheduler.Core.Contracts.Services;
using SiT.Scheduler.Core.Contracts.Transformations;
using SiT.Scheduler.Data.Contracts.Repositories;
using SiT.Scheduler.Data.Models;
using SiT.Scheduler.Validation.Contracts;

public class SongService : BaseService<Song, IDefaultExternalRequirement, ISongPrototype>, ISongService
{
    public SongService(IRepository<Song> repository, IEntityValidatorFactory entityValidatorFactory, IDataTransformerFactory dataTransformerFactory)
        : base(repository, entityValidatorFactory, dataTransformerFactory)
    {
    }

    protected override Song InitializeEntity(ISongPrototype prototype) => new();

    protected override void ApplyPrototype(ISongPrototype prototype, Song entity)
    {
        entity.Name = prototype.Name;
    }

    protected override Expression<Func<Song, bool>> ConstructFilter(IDefaultExternalRequirement externalRequirement) => null;
}
