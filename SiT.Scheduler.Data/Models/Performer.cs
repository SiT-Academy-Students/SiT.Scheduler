namespace SiT.Scheduler.Data.Models;

using System;
using SiT.Scheduler.Data.Contracts.Models;
using System.Collections.Generic;

public class Performer : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }    
    public ICollection<Song> Songs { get; set; }
}
