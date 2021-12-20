namespace SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;
using System;

public interface ISongLayout
{
    public Guid Id { get; }
    public string Name { get; }
    public string Author { get; }
}
