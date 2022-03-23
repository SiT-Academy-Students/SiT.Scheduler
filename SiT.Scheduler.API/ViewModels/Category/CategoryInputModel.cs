namespace SiT.Scheduler.API.ViewModels.Category;

using JetBrains.Annotations;

public class CategoryInputModel
{
    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public string Name { get; set; }

    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public string Description { get; set; }
}
