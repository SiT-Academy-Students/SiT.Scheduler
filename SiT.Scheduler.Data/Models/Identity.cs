namespace SiT.Scheduler.Data.Models;

using System;
using System.Collections.Generic;
using SiT.Scheduler.Data.Contracts.Models;

public class Identity : IEntity
{
    public Guid Id { get; set;  }
    public Guid ExternalId { get; set; }
    public string DisplayName { get; set; }

    public ICollection<Tenant> Tenants { get; set; } = new List<Tenant>();
}