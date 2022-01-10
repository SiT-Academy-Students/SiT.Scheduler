namespace SiT.Scheduler.API.Factories;

using SiT.Scheduler.API.Contracts.Factories;
using SiT.Scheduler.API.ViewModels.Song;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;

public class SongFactory : ISongFactory
{
    public SongViewModel ToViewModel(ISongLayout layout)
    {
        if (layout is null) return null;

        var viewModel = new SongViewModel
        {
            Id = layout.Id,
            Name = layout.Name,
            Author = layout.Author,
        };

        return viewModel;
    }
}
