namespace SiT.Scheduler.API.Factories;

using System;
using SiT.Scheduler.API.Contracts.Factories;
using SiT.Scheduler.API.ViewModels.Song;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;
using SiT.Scheduler.Utilities;

public class SongFactory : ISongFactory
{
    private readonly IGenreFactory _genreFactory;
    private readonly IPerformerFactory _performerFactory;

    public SongFactory(IGenreFactory genreFactory, IPerformerFactory performerFactory)
    {
        this._genreFactory = genreFactory ?? throw new ArgumentNullException(nameof(genreFactory));
        this._performerFactory = performerFactory ?? throw new ArgumentNullException(nameof(performerFactory));
    }

    public SongViewModel ToViewModel(ISongLayout layout)
    {
        if (layout is null) return null;

        var viewModel = new SongViewModel
        {
            Id = layout.Id,
            Name = layout.Name,
        };

        foreach (var genre in layout.Genres.OrEmptyIfNull().IgnoreNullValues())
        {
            var genreViewModel = this._genreFactory.ToViewModel(genre);
            if (genreViewModel is not null) viewModel.AddGenre(genreViewModel);
        }

        foreach (var performer in layout.Performers.OrEmptyIfNull().IgnoreNullValues())
        {
            var performerViewModel = this._performerFactory.ToViewModel(performer);
            if (performerViewModel is not null) viewModel.AddPerformer(performerViewModel);
        }

        return viewModel;
    }
}
