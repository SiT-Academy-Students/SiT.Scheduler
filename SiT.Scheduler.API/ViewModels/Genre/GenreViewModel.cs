namespace SiT.Scheduler.API.ViewModels.Genre;

using System;
using JetBrains.Annotations;

public class GenreViewModel
{
    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    public Guid Id { get; set; }

    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    public string Name { get; set; }
    
    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    public string Description { get; set; }
}
