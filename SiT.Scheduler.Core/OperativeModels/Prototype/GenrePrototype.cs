namespace SiT.Scheduler.Core.OperativeModels.Prototype;

using SiT.Scheduler.Core.Contracts.OperativeModels.Prototypes;

public record GenrePrototype(string Name, string Description) : IGenrePrototype;
