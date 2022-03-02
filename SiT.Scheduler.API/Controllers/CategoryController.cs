namespace SiT.Scheduler.API.Controllers;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SiT.Scheduler.API.Contracts.Factories;
using SiT.Scheduler.API.Extensions;
using SiT.Scheduler.API.ViewModels.Category;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;
using SiT.Scheduler.Core.Contracts.Services;
using SiT.Scheduler.Core.OperativeModels.ExternalRequirements;
using SiT.Scheduler.Core.OperativeModels.Prototype;
using SiT.Scheduler.Utilities;

[ApiController]
[Route("categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ICategoryFactory _categoryFactory;

    public CategoryController(ICategoryService categoryService, ICategoryFactory categoryFactory)
    {
        this._categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        this._categoryFactory = categoryFactory ?? throw new ArgumentNullException(nameof(categoryFactory));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CategoryInputModel inputModel, CancellationToken cancellationToken)
    {
        if (inputModel is null) return this.BadRequest("Invalid input model.");

        var prototype = new CategoryPrototype(inputModel.Name, inputModel.Description);
        var createCategory = await this._categoryService.CreateAsync(prototype, cancellationToken);
        if (!createCategory.IsSuccessful) return this.Error(createCategory);

        return this.Ok(createCategory.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetManyAsync(CancellationToken cancellationToken)
    {
        var getCategories = await this._categoryService.GetManyAsync<ICategoryMinifiedLayout>(ExternalRequirement.Default, cancellationToken);
        if (!getCategories.IsSuccessful) return this.Error(getCategories);

        var viewModels = getCategories.Data.OrEmptyIfNull().IgnoreNullValues().Select(this._categoryFactory.ToViewModel);
        return this.Ok(viewModels);
    }
}
