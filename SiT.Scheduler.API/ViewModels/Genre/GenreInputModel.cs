namespace SiT.Scheduler.API.ViewModels.Genre;

using System;
using JetBrains.Annotations;

public class GenreInputModel
{
    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    public string Name { get; set; }

    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    public string Description { get; set; }
}
