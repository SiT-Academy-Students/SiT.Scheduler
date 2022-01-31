namespace SiT.Scheduler.Core.Contracts.Services;
using SiT.Scheduler.Core.Contracts.OperativeModels.ExternalRequirements;
using SiT.Scheduler.Core.Contracts.OperativeModels.Prototypes;
using SiT.Scheduler.Data.Models;

internal interface IPerformerService : IService<Performer, IDefaultExternalRequirement, IPerformerPrototype>
{
}
