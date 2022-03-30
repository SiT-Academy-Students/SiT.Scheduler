namespace SiT.Scheduler.Core.Contracts.OperativeModels.Prototypes;

public interface ITenantPrototype
{
    string Name { get; }
    bool IsSystem { get; }
}