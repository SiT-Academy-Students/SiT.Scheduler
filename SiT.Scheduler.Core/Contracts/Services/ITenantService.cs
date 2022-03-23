﻿namespace SiT.Scheduler.Core.Contracts.Services;

using SiT.Scheduler.Core.Contracts.OperativeModels.ExternalRequirements;
using SiT.Scheduler.Core.Contracts.OperativeModels.Prototypes;
using SiT.Scheduler.Data.Models;

public interface ITenantService : IService<Tenant, IDefaultExternalRequirement, ITenantPrototype>
{
    
}