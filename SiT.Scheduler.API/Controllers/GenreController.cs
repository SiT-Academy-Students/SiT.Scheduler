namespace SiT.Scheduler.API.Controllers;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SiT.Scheduler.API.Contracts.Factories;
using SiT.Scheduler.API.Extensions;
using SiT.Scheduler.API.ViewModels.Genre;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;
using SiT.Scheduler.Core.Contracts.Services;
using SiT.Scheduler.Core.OperativeModels.ExternalRequirements;
using SiT.Scheduler.Core.OperativeModels.Prototype;
using SiT.Scheduler.Utilities;

[ApiController]
[Route("genres")]
public class GenreController : ControllerBase
{
    private readonly IGenreService _genreService;
    private readonly IGenreFactory _genreFactory;

    public GenreController(IGenreService genreService, IGenreFactory genreFactory)
    {
        this._genreService = genreService ?? throw new ArgumentNullException(nameof(genreService));
        this._genreFactory = genreFactory ?? throw new ArgumentNullException(nameof(genreFactory));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] GenreInputModel inputModel, CancellationToken cancellationToken)
    {
        if (inputModel is null) return this.BadRequest("Invalid input model.");

        var prototype = new GenrePrototype(inputModel.Name, inputModel.Description);
        var createGenre = await this._genreService.CreateAsync(prototype, cancellationToken);
        if (!createGenre.IsSuccessful) return this.Error(createGenre);

        return this.Ok(createGenre.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetManyAsync(CancellationToken cancellationToken)
    {
        var getGenres = await this._genreService.GetManyAsync<IGenreMinifiedLayout>(ExternalRequirement.Default, cancellationToken);
        if (!getGenres.IsSuccessful) return this.Error(getGenres);

        var viewModels = getGenres.Data.OrEmptyIfNull().IgnoreNullValues().Select(this._genreFactory.ToViewModel);
        return this.Ok(viewModels);
    }
}
