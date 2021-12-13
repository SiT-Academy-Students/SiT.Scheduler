namespace SiT.Scheduler.Core.OperativeModels.Prototype;

using SiT.Scheduler.Core.Contracts.OperativeModels.Prototypes;

public record SongPrototype(string Name, string Author) : ISongPrototype;
