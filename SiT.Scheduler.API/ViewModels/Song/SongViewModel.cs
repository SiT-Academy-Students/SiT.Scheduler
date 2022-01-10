namespace SiT.Scheduler.API.ViewModels.Song;

using System;
using JetBrains.Annotations;

public class SongViewModel
{
    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    public Guid Id { get; set; }

    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    public string Name { get; set; }
    
    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    public string Author { get; set; }
}
