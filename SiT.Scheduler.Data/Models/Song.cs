namespace SiT.Scheduler.Data.Models;

using SiT.Scheduler.Data.Contracts.Models;
using System;
using System.Collections.Generic;

public class Song : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<Performer> Performers { get; set; }
}
