namespace SiT.Scheduler.API.Factories;

using SiT.Scheduler.API.Contracts.Factories;
using SiT.Scheduler.API.ViewModels.Genre;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;

public class GenreFactory : IGenreFactory
{
    public GenreViewModel ToViewModel(IGenreMinifiedLayout layout)
    {
        if (layout is null) return null;

        var viewModel = new GenreViewModel
        {
            Id = layout.Id,
            Name = layout.Name,
            Description = layout.Description,
        };

        return viewModel;
    }
}
