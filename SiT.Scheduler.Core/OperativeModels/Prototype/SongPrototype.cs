namespace SiT.Scheduler.Core.OperativeModels.Prototype;

using System;
using System.Collections.Generic;
using System.Linq;
using SiT.Scheduler.Core.Contracts.OperativeModels.Prototypes;
using SiT.Scheduler.Utilities;

public record SongPrototype : ISongPrototype
{
    public SongPrototype(string name, IEnumerable<Guid> genres, IEnumerable<Guid> performers)
    {
        this.Name = name;
        this.Genres = genres.OrEmptyIfNull().IgnoreDefaultValues().ToList().AsReadOnly();
        this.Performers = performers.OrEmptyIfNull().IgnoreDefaultValues().ToList().AsReadOnly();
    }

    public string Name { get; }
    public IReadOnlyCollection<Guid> Genres { get; }
    public IReadOnlyCollection<Guid> Performers { get; }
}
