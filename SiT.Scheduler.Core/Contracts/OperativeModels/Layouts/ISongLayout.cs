namespace SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;
using System;
using System.Collections.Generic;

public interface ISongLayout
{
    Guid Id { get; }
    string Name { get; }
    IReadOnlyCollection<IGenreMinifiedLayout> Genres { get; }
    IReadOnlyCollection<IPerformerMinifiedLayout> Performers { get; }
    IReadOnlyCollection<ICategoryMinifiedLayout> Categories { get; }
}
