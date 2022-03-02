namespace SiT.Scheduler.API.Controllers;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SiT.Scheduler.API.Contracts.Factories;
using SiT.Scheduler.API.Extensions;
using SiT.Scheduler.API.ViewModels.Song;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;
using SiT.Scheduler.Core.Contracts.Services;
using SiT.Scheduler.Core.OperativeModels.ExternalRequirements;
using SiT.Scheduler.Core.OperativeModels.Prototype;
using SiT.Scheduler.Utilities;

[ApiController]
[Route("songs")]
public class SongController : ControllerBase
{
    private readonly ISongService _songService;
    private readonly ISongFactory _songFactory;

    public SongController(ISongService songService, ISongFactory songFactory)
    {
        this._songService = songService ?? throw new ArgumentNullException(nameof(songService));
        this._songFactory = songFactory ?? throw new ArgumentNullException(nameof(songFactory));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] SongInputModel inputModel, CancellationToken cancellationToken)
    {
        if (inputModel is null) return this.BadRequest("Invalid input model.");

        var prototype = new SongPrototype(inputModel.Name, inputModel.Genres, inputModel.Performers);
        var createSong = await this._songService.CreateAsync(prototype, cancellationToken);
        if (!createSong.IsSuccessful) return this.Error(createSong);

        return this.Ok(createSong.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetManyAsync(CancellationToken cancellationToken)
    {
        var getSongs = await this._songService.GetManyAsync<ISongLayout>(ExternalRequirement.Default, cancellationToken);
        if (!getSongs.IsSuccessful) return this.Error(getSongs);

        var viewModels = getSongs.Data.OrEmptyIfNull().IgnoreNullValues().Select(this._songFactory.ToViewModel);
        return this.Ok(viewModels);
    }
}
