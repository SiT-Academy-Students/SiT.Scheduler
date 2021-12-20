namespace SiT.Scheduler.Core.OperativeModels.ExternalRequirements;
using SiT.Scheduler.Core.Contracts.OperativeModels.ExternalRequirements;

public class ExternalRequirement : IDefaultExternalRequirement
{
    public static IDefaultExternalRequirement Default => new ExternalRequirement();
}
