namespace SiT.Scheduler.Data.Contracts.Models;

using System;
using SiT.Scheduler.Data.Models;

public interface ITenantEntity
{
    Guid TenantId { get; set; }
    Tenant Tenant { get; set; }
}