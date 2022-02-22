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
    private readonly ICategoryFactory _categoryFactory;

    public SongFactory(IGenreFactory genreFactory, IPerformerFactory performerFactory, ICategoryFactory categoryFactory)
    {
        this._genreFactory = genreFactory ?? throw new ArgumentNullException(nameof(genreFactory));
        this._performerFactory = performerFactory ?? throw new ArgumentNullException(nameof(performerFactory));
        this._categoryFactory = categoryFactory ?? throw new ArgumentNullException(nameof(categoryFactory));
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

        foreach (var category in layout.Categories.OrEmptyIfNull().IgnoreNullValues())
        {
            var categoryViewModel = this._categoryFactory.ToViewModel(category);
            if (categoryViewModel is not null) viewModel.AddCategory(categoryViewModel);
        }

        return viewModel;
    }
}
