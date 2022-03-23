namespace SiT.Scheduler.Data.Models;

using System;
using SiT.Scheduler.Data.Contracts.Models;

public class Identity : IEntity
{
    public Guid Id { get; set;  }
    public string DisplayName { get; set; }
}