namespace SiT.Scheduler.Data.Models;

using System;
using SiT.Scheduler.Data.Contracts.Models;
using System.Collections.Generic;

public class Genre : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<Song> Songs { get; set; }
}
