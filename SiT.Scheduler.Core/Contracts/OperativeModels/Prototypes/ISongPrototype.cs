namespace SiT.Scheduler.Core.Contracts.OperativeModels.Prototypes;

using System;
using System.Collections.Generic;

public interface ISongPrototype
{
    string Name { get; }
    IReadOnlyCollection<Guid> Genres { get; }
    IReadOnlyCollection<Guid> Performers { get; }
    IReadOnlyCollection<Guid> Categories { get; }
}