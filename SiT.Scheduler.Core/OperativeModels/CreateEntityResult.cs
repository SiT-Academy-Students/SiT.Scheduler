namespace SiT.Scheduler.Core.OperativeModels;
using System;
using SiT.Scheduler.Core.Contracts.OperativeModels;

public record CreateEntityResult(Guid EntityId) : ICreateEntityResult;
