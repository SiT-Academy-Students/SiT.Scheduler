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

public class GenreService : BaseService<Genre, IDefaultExternalRequirement, IGenrePrototype>, IGenreService
{
    public GenreService(IRepository<Genre> repository, IEntityValidatorFactory entityValidatorFactory, IDataTransformerFactory dataTransformerFactory)
        : base(repository, entityValidatorFactory, dataTransformerFactory)
    {
    }

    protected override Genre InitializeEntity(IGenrePrototype prototype) => new();

    protected override Task<IOperationResult> ApplyPrototypeAsync(IGenrePrototype prototype, Genre entity, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult();

        entity.Name = prototype.Name;
        entity.Description = prototype.Description;

        return Task.FromResult<IOperationResult>(operationResult);
    }

    protected override OperationResult<Expression<Func<Genre, bool>>> ConstructFilter(IDefaultExternalRequirement externalRequirement) => new();
}
