namespace SiT.Scheduler.Core.Services;

using System.Threading;
using System.Threading.Tasks;
using SiT.Scheduler.Core.Contracts.Authentication;
using SiT.Scheduler.Core.Contracts.OperativeModels.ExternalRequirements;
using SiT.Scheduler.Core.Contracts.OperativeModels.Prototypes;
using SiT.Scheduler.Core.Contracts.Services;
using SiT.Scheduler.Core.Contracts.Transformations;
using SiT.Scheduler.Data.Contracts.Repositories;
using SiT.Scheduler.Data.Models;
using SiT.Scheduler.Utilities.OperationResults;
using SiT.Scheduler.Validation.Contracts;

public class GenreService : BaseTenantEntityService<Genre, IDefaultExternalRequirement, IGenrePrototype>, IGenreService
{
    public GenreService(IRepository<Genre> repository, IEntityValidatorFactory entityValidatorFactory, IDataTransformerFactory dataTransformerFactory, ITenantContext tenantContext)
        : base(repository, entityValidatorFactory, dataTransformerFactory, tenantContext)
    {
    }

    protected override Genre InitializeEntity(IGenrePrototype prototype) => new();

    protected override async Task<IOperationResult> ApplyPrototypeAsync(IGenrePrototype prototype, Genre entity, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult();

        var basePrototypeApplication = await base.ApplyPrototypeAsync(prototype, entity, cancellationToken);
        if (!basePrototypeApplication.IsSuccessful) return operationResult.AppendErrors(basePrototypeApplication);

        entity.Name = prototype.Name;
        entity.Description = prototype.Description;

        return operationResult;
    }
}
