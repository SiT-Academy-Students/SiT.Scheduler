namespace SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;

using System.Collections.Generic;

public interface ISongLayout : ILayout
{
    string Name { get; }
    IReadOnlyCollection<IGenreMinifiedLayout> Genres { get; }
    IReadOnlyCollection<IPerformerMinifiedLayout> Performers { get; }
    IReadOnlyCollection<ICategoryMinifiedLayout> Categories { get; }
}
