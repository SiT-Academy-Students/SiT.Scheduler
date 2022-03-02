namespace SiT.Scheduler.StorageManagement.Configuration;

using JetBrains.Annotations;

public class StorageManagementConfiguration
{
    public const string Section = "StorageManagement";

    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public string Provider { get; set; }
}