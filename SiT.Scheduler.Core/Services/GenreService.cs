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

public class GenreService : BaseService<Genre, IDefaultExternalRequirement, IGenrePrototype>, IGenreService
{
    public GenreService(IRepository<Genre> repository, IEntityValidatorFactory entityValidatorFactory, IDataTransformerFactory dataTransformerFactory)
        : base(repository, entityValidatorFactory, dataTransformerFactory)
    {
    }

    protected override Genre InitializeEntity(IGenrePrototype prototype) => new();

    protected override void ApplyPrototype(IGenrePrototype prototype, Genre entity)
    {
        entity.Name = prototype.Name;
        entity.Description = prototype.Description;
    }

    protected override Expression<Func<Genre, bool>> ConstructFilter(IDefaultExternalRequirement externalRequirement) => null;
}
