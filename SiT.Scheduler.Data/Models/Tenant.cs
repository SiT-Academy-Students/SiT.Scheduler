namespace SiT.Scheduler.Data.Models;

using System;
using System.Collections.Generic;
using SiT.Scheduler.Data.Contracts.Models;

public class Tenant : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsSystem { get; set; }

    public ICollection<Identity> Identities { get; set; } = new List<Identity>();
    public ICollection<Category> Categories { get; set; } = new List<Category>();
}