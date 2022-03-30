namespace SiT.Scheduler.Core.Contracts.OperativeModels.Prototypes;

using System;

public interface IIdentityPrototype
{
    Guid Id { get; }
    Guid TenantId { get; }
    string DisplayName { get; }
}