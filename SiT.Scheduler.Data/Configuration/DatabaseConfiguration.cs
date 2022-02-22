namespace SiT.Scheduler.Data.Configuration;

using JetBrains.Annotations;

public class DatabaseConfiguration
{
    public const string Section = "Database";

    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public string Provider { get; set; }

    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public string ConnectionString { get; set; }
}
