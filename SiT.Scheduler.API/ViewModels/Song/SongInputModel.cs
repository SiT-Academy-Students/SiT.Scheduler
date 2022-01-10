namespace SiT.Scheduler.API.ViewModels.Song;

using JetBrains.Annotations;

public class SongInputModel
{
    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    public string Name { get; set; }

    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    public string Author { get; set; }
}
