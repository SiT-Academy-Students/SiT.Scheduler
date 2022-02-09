namespace SiT.Scheduler.API.ViewModels.Song;

using System;
using System.Collections.Generic;
using JetBrains.Annotations;

public class SongInputModel
{
    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public string Name { get; set; }

    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public IEnumerable<Guid> Genres { get; set; }

    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public IEnumerable<Guid> Performers { get; set; }
}
