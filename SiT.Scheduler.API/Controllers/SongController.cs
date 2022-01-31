namespace SiT.Scheduler.API.Controllers;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SiT.Scheduler.API.Contracts.Factories;
using SiT.Scheduler.API.ViewModels.Song;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;
using SiT.Scheduler.Core.Contracts.Services;
using SiT.Scheduler.Core.OperativeModels.ExternalRequirements;
using SiT.Scheduler.Core.OperativeModels.Prototype;
using SiT.Scheduler.Core.Options;
using SiT.Scheduler.Data.Models;
using SiT.Scheduler.Utilities;

[ApiController]
[Route("songs")]
public class SongController : ControllerBase
{
    private readonly ISongService _songService;
    private readonly IGenreService _genreService;
    private readonly IPerformerService _performerService;
    private readonly ISongFactory _songFactory;

    public SongController(ISongService songService, IGenreService genreService, IPerformerService performerService, ISongFactory songFactory)
    {
        this._songService = songService ?? throw new ArgumentNullException(nameof(songService));
        this._genreService = genreService ?? throw new ArgumentNullException(nameof(genreService));
        this._performerService = performerService ?? throw new ArgumentNullException(nameof(performerService));
        this._songFactory = songFactory ?? throw new ArgumentNullException(nameof(songFactory));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] SongInputModel inputModel, CancellationToken cancellationToken)
    {
        if (inputModel is null) return this.BadRequest("Invalid input model.");

        var queryGenresOptions = new QueryEntityOptions<Genre>();
        queryGenresOptions.AddFilter(x => inputModel.Genres.Contains(x.Id));
        var getGenres = await this._genreService.GetManyAsync(ExternalRequirement.Default, cancellationToken, queryGenresOptions);
        if (!getGenres.IsSuccessful) return this.BadRequest(getGenres.ToString());

        var queryPerformersOptions = new QueryEntityOptions<Performer>();
        queryPerformersOptions.AddFilter(x => inputModel.Performers.Contains(x.Id));
        var getPerformers = await this._performerService.GetManyAsync(ExternalRequirement.Default, cancellationToken, queryPerformersOptions);
        if (!getPerformers.IsSuccessful) return this.BadRequest(getPerformers.ToString());

        var prototype = new SongPrototype(inputModel.Name, getGenres.Data, getPerformers.Data);
        var createSong = await this._songService.CreateAsync(prototype, cancellationToken);
        if (!createSong.IsSuccessful)
            return this.BadRequest(createSong.ToString());

        return this.Ok(createSong.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetManyAsync(CancellationToken cancellationToken)
    {
        var getSongs = await this._songService.GetManyAsync<ISongLayout>(ExternalRequirement.Default, cancellationToken);
        if (!getSongs.IsSuccessful)
            return this.BadRequest(getSongs.ToString());

        var viewModels = getSongs.Data.OrEmptyIfNull().IgnoreNullValues().Select(this._songFactory.ToViewModel);
        return this.Ok(viewModels);
    }
}
