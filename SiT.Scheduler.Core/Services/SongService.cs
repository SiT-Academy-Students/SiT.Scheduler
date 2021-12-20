namespace SiT.Scheduler.Core.Services;
using System;
using System.Linq.Expressions;
using SiT.Scheduler.Core.Contracts.OperativeModels.ExternalRequirements;
using SiT.Scheduler.Core.Contracts.OperativeModels.Prototypes;
using SiT.Scheduler.Core.Contracts.Services;
using SiT.Scheduler.Core.Contracts.Transformations;
using SiT.Scheduler.Data.Contracts.Repositories;
using SiT.Scheduler.Data.Models;

public class SongService : BaseService<Song, IDefaultExternalRequirement, ISongPrototype>, ISongService
{
    public SongService(IRepository<Song> repository, IDataTransformerFactory dataTransformerFactory)
        : base(repository, dataTransformerFactory)
    {
    }

    protected override Song Initialize(ISongPrototype prototype)
    {
        var song = new Song();
        return song;
    }

    protected override void ApplyPrototype(ISongPrototype prototype, Song entity)
    {
        entity.Name = prototype.Name;
        entity.Author = prototype.Author;
    }

    protected override Expression<Func<Song, bool>> ConstructFilter(IDefaultExternalRequirement externalRequirement) => null;
}
