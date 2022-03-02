namespace SiT.Scheduler.StorageManagement.CloudinaryImpl.Configuration;

using JetBrains.Annotations;

public class CloudinaryConfiguration
{
    public const string Section = "Cloudinary";

    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    public string CloudName { get; set; }

    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    public string ApiKey { get; set; }

    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    public string ApiSecret { get; set; }

    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    public string AssetFolder { get; set; }
}