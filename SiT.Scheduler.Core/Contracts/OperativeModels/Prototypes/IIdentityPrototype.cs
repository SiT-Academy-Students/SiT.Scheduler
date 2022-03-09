namespace SiT.Scheduler.Core.Contracts.OperativeModels.Prototypes;

using System;

public interface IIdentityPrototype
{
    Guid Id { get; }
    string DisplayName { get; }
}