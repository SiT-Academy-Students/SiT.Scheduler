namespace SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;

using System;

public interface IGenreLayout
{
    public Guid Id { get; }
    public string Name { get; }
    public string Description  { get; }
}
