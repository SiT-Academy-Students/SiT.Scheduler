namespace SiT.Scheduler.API.ViewModels.Song;

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using SiT.Scheduler.API.ViewModels.Genre;
using SiT.Scheduler.API.ViewModels.Performer;

public class SongViewModel
{
    private readonly List<GenreViewModel> _genres = new();
    private readonly List<PerformerViewModel> _performers = new();

    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    public Guid Id { get; set; }

    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    public string Name { get; set; }

    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    public IReadOnlyCollection<GenreViewModel> Genre => this._genres.AsReadOnly();

    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    public IReadOnlyCollection<PerformerViewModel> Performers => this._performers.AsReadOnly();

    public bool AddGenre(GenreViewModel genreViewModel)
    {
        if (genreViewModel is null) return false;
        this._genres.Add(genreViewModel);
        return true;
    }

    public bool AddPerformer(PerformerViewModel performerViewModel)
    {
        if (performerViewModel is null) return false;
        this._performers.Add(performerViewModel);
        return true;
    }
}
