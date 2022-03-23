namespace SiT.Scheduler.Resources.LabelProviders;

using System.Resources;

[LabelsProvider]
public static class GeneralErrors
{
    private static readonly ResourceManager _resourceManager = ResourcesHelper.Construct("GeneralErrors");

    public static string EntityDoesNotExist => _resourceManager.GetString(nameof(EntityDoesNotExist));
    public static string InvalidArgument => _resourceManager.GetString(nameof(InvalidArgument));
    public static string IndexIsOutOfRange => _resourceManager.GetString(nameof(IndexIsOutOfRange));
    public static string InvalidOperation => _resourceManager.GetString(nameof(InvalidOperation));
    public static string WrongFormat => _resourceManager.GetString(nameof(WrongFormat));
    public static string UnsupportedLayout => _resourceManager.GetString(nameof(UnsupportedLayout));
    public static string MissingTenantContext => _resourceManager.GetString(nameof(MissingTenantContext));
}
