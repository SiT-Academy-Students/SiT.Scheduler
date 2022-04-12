namespace SiT.Scheduler.Data.Models;

using SiT.Scheduler.Data.Contracts.Models;
using System;
using System.Collections.Generic;

public class Song : IEntity, ITenantEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; }

    public ICollection<Genre> Genres { get; set; } = new List<Genre>();
    public ICollection<Performer> Performers { get; set; } = new List<Performer>();
    public ICollection<Category> Categories { get; set; } = new List<Category>();
}
