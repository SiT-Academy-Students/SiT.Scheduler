namespace SiT.Scheduler.Tests.Randomization;
using SiT.Scheduler.Data.Models;
using TryAtSoftware.Randomizer.Core.Interfaces;

public class SongInstanceBuilder : IInstanceBuilder<Song>
{
    public Song PrepareNewInstance() => new ();
}
