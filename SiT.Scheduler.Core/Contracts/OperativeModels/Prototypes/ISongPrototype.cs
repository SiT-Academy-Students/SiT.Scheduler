namespace SiT.Scheduler.Core.Contracts.OperativeModels.Prototypes;

using System.Collections.Generic;
using SiT.Scheduler.Data.Models;

public interface ISongPrototype
{
    string Name { get; }
    IReadOnlyCollection<Genre> Genres { get; }
    IReadOnlyCollection<Performer> Performers { get; }
}
