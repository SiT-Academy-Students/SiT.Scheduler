namespace SiT.Scheduler.API.Controllers;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

    public SongController(ISongService songService)
    {
        this._songService = songService ?? throw new ArgumentNullException(nameof(songService));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] SongInputModel inputModel, CancellationToken cancellationToken)
    {
        if (inputModel is null) return this.BadRequest("Invalid input model.");

        var prototype = new SongPrototype(inputModel.Name, inputModel.Author);
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

        var viewModels = getSongs.Data.OrEmptyIfNull().IgnoreNullValues().Select(this.ToViewModel);
        return this.Ok(viewModels);
    }

    private SongViewModel ToViewModel(ISongLayout songLayout)
    {
        if (songLayout is null) return null;

        var viewModel = new SongViewModel
        {
            Id = songLayout.Id,
            Name = songLayout.Name,
            Author = songLayout.Author,
        };

        return viewModel;
    }
}
