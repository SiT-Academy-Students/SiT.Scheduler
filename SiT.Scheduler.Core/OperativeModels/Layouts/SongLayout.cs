namespace SiT.Scheduler.Core.OperativeModels.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;
using SiT.Scheduler.Utilities;

public record SongLayout : ISongLayout
{
    public SongLayout(Guid id, string name, IEnumerable<IGenreMinifiedLayout> genres, IEnumerable<IPerformerMinifiedLayout> performers)
    {
        this.Id = id;
        this.Name = name;
        this.Genres = genres.OrEmptyIfNull().IgnoreNullValues().ToList().AsReadOnly();
        this.Performers = performers.OrEmptyIfNull().IgnoreNullValues().ToList().AsReadOnly();
    }

    public Guid Id { get; }
    public string Name { get; }
    public IReadOnlyCollection<IGenreMinifiedLayout> Genres { get; }
    public IReadOnlyCollection<IPerformerMinifiedLayout> Performers { get; }
}