namespace SiT.Scheduler.Core.OperativeModels.Prototype;

using SiT.Scheduler.Core.Contracts.OperativeModels.Prototypes;

public record TenantPrototype(string Name, bool IsSystem) : ITenantPrototype;
