namespace SiT.Scheduler.API.ViewModels.Category;

using System;
using JetBrains.Annotations;

public class CategoryInputModel
{
    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    public string Name { get; set; }

    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    public string Description { get; set; }
}
