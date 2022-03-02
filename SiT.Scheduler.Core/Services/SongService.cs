namespace SiT.Scheduler.Core.Services;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using SiT.Scheduler.Core.Contracts.OperativeModels.ExternalRequirements;
using SiT.Scheduler.Core.Contracts.OperativeModels.Prototypes;
using SiT.Scheduler.Core.Contracts.Services;
using SiT.Scheduler.Core.Contracts.Transformations;
using SiT.Scheduler.Core.OperativeModels.ExternalRequirements;
using SiT.Scheduler.Core.Options;
using SiT.Scheduler.Data.Contracts.Repositories;
using SiT.Scheduler.Data.Models;
using SiT.Scheduler.Utilities;
using SiT.Scheduler.Utilities.OperationResults;
using SiT.Scheduler.Validation.Contracts;

public class SongService : BaseService<Song, IDefaultExternalRequirement, ISongPrototype>, ISongService
{
    private readonly IGenreService _genreService;
    private readonly IPerformerService _performerService;
    private readonly ICategoryService _categoryService;

    public SongService(IRepository<Song> repository, IGenreService genreService, IPerformerService performerService, ICategoryService categoryService, IEntityValidatorFactory entityValidatorFactory, IDataTransformerFactory dataTransformerFactory)
        : base(repository, entityValidatorFactory, dataTransformerFactory)
    {
        this._genreService = genreService ?? throw new ArgumentNullException(nameof(genreService));
        this._performerService = performerService ?? throw new ArgumentNullException(nameof(performerService));
        this._categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
    }

    protected override Song InitializeEntity(ISongPrototype prototype) => new();

    protected override async Task<IOperationResult> ApplyPrototypeAsync(ISongPrototype prototype, Song entity, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult();

        var queryGenresOptions = new QueryEntityOptions<Genre>();
        queryGenresOptions.AddFilter(x => prototype.Genres.Contains(x.Id));
        var getGenres = await this._genreService.GetManyAsync(ExternalRequirement.Default, cancellationToken, queryGenresOptions);
        if (!getGenres.IsSuccessful) return operationResult.AppendErrors(getGenres);

        var queryPerformersOptions = new QueryEntityOptions<Performer>();
        queryPerformersOptions.AddFilter(x => prototype.Performers.Contains(x.Id));
        var getPerformers = await this._performerService.GetManyAsync(ExternalRequirement.Default, cancellationToken, queryPerformersOptions);
        if (!getPerformers.IsSuccessful) return operationResult.AppendErrors(getPerformers);

        var queryCategoriesOptions = new QueryEntityOptions<Category>();
        queryCategoriesOptions.AddFilter(x => prototype.Categories.Contains(x.Id));
        var getCategories = await this._categoryService.GetManyAsync(ExternalRequirement.Default, cancellationToken, queryCategoriesOptions);
        if (!getCategories.IsSuccessful) return operationResult.AppendErrors(getCategories);

        entity.Name = prototype.Name;
        entity.Genres.Clear();
        foreach (var genre in getGenres.Data.OrEmptyIfNull().IgnoreNullValues()) entity.Genres.Add(genre);

        entity.Performers.Clear();
        foreach (var performer in getPerformers.Data.OrEmptyIfNull().IgnoreNullValues()) entity.Performers.Add(performer);

        entity.Categories.Clear();
        foreach (var category in getCategories.Data.OrEmptyIfNull().IgnoreNullValues()) entity.Categories.Add(category);

        return operationResult;
    }

    protected override Expression<Func<Song, bool>> ConstructFilter(IDefaultExternalRequirement externalRequirement) => null;
}
