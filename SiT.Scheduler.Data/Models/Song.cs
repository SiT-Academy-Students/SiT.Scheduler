namespace SiT.Scheduler.Data.Models;

using SiT.Scheduler.Data.Contracts.Models;
using System;

public class Song : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
}
