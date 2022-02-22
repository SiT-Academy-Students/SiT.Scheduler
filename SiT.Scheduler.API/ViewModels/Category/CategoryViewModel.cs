namespace SiT.Scheduler.API.ViewModels.Category;

using System;
using JetBrains.Annotations;

public class CategoryViewModel
{
    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    public Guid Id { get; set; }

    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    public string Name { get; set; }

    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    public string Description { get; set; }
}
