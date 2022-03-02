namespace SiT.Scheduler.API.Controllers;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SiT.Scheduler.API.Contracts.Factories;
using SiT.Scheduler.API.Extensions;
using SiT.Scheduler.API.ViewModels.Performer;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;
using SiT.Scheduler.Core.Contracts.Services;
using SiT.Scheduler.Core.OperativeModels.ExternalRequirements;
using SiT.Scheduler.Core.OperativeModels.Prototype;
using SiT.Scheduler.Utilities;

[ApiController]
[Route("performers")]
public class PerformerController : ControllerBase
{
    private readonly IPerformerService _performerService;
    private readonly IPerformerFactory _performerFactory;

    public PerformerController(IPerformerService performerService, IPerformerFactory performerFactory)
    {
        this._performerService = performerService ?? throw new ArgumentNullException(nameof(performerService));
        this._performerFactory = performerFactory ?? throw new ArgumentNullException(nameof(performerFactory));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] PerformerInputModel inputModel, CancellationToken cancellationToken)
    {
        if (inputModel is null) return this.BadRequest("Invalid input model.");

        var prototype = new PerformerPrototype(inputModel.Name);
        var createPerformer = await this._performerService.CreateAsync(prototype, cancellationToken);
        if (!createPerformer.IsSuccessful) return this.Error(createPerformer);

        return this.Ok(createPerformer.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetManyAsync(CancellationToken cancellationToken)
    {
        var getPerformers = await this._performerService.GetManyAsync<IPerformerMinifiedLayout>(ExternalRequirement.Default, cancellationToken);
        if (!getPerformers.IsSuccessful) return this.Error(getPerformers);

        var viewModels = getPerformers.Data.OrEmptyIfNull().IgnoreNullValues().Select(this._performerFactory.ToViewModel);
        return this.Ok(viewModels);
    }
}
