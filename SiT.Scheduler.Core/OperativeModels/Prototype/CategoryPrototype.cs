namespace SiT.Scheduler.Core.OperativeModels.Prototype;

using SiT.Scheduler.Core.Contracts.OperativeModels.Prototypes;

public record CategoryPrototype(string Name, string Description) : ICategoryPrototype;
