namespace SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;

using System;

public interface IGenreMinifiedLayout
{
    Guid Id { get; }
    string Name { get; }
    string Description  { get; }
}
