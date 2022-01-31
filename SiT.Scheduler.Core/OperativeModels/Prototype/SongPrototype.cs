namespace SiT.Scheduler.Core.OperativeModels.Prototype;
using System.Collections.Generic;
using System.Linq;
using SiT.Scheduler.Core.Contracts.OperativeModels.Prototypes;
using SiT.Scheduler.Data.Models;
using SiT.Scheduler.Utilities;

public record SongPrototype : ISongPrototype
{
    public SongPrototype(string name, IEnumerable<Genre> genres, IEnumerable<Performer> performers)
    {
        this.Name = name;
        this.Genres = genres.OrEmptyIfNull().IgnoreNullValues().ToList().AsReadOnly();
        this.Performers = performers.OrEmptyIfNull().IgnoreNullValues().ToList().AsReadOnly();
    }

    public string Name { get; }
    public IReadOnlyCollection<Genre> Genres { get; }
    public IReadOnlyCollection<Performer> Performers { get; }
}
