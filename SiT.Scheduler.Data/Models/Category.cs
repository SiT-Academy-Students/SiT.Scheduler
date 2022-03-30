namespace SiT.Scheduler.Data.Models;

using System;
using SiT.Scheduler.Data.Contracts.Models;
using System.Collections.Generic;

public class Category : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; }

    public ICollection<Song> Songs { get; set; } = new List<Song>();
}
