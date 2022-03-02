namespace SiT.Scheduler.API.Factories;

using SiT.Scheduler.API.Contracts.Factories;
using SiT.Scheduler.API.ViewModels.Category;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;

public class CategoryFactory : ICategoryFactory
{
    public CategoryViewModel ToViewModel(ICategoryMinifiedLayout layout)
    {
        if (layout is null) return null;

        var viewModel = new CategoryViewModel
        {
            Id = layout.Id,
            Name = layout.Name,
            Description = layout.Description,
        };

        return viewModel;
    }
}
