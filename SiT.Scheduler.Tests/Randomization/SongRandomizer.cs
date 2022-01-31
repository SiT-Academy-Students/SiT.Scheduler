namespace SiT.Scheduler.Tests.Randomization;
using SiT.Scheduler.Data.Models;
using TryAtSoftware.Randomizer.Core;
using TryAtSoftware.Randomizer.Core.Interfaces;
using TryAtSoftware.Randomizer.Core.Primitives;

public class SongRandomizer : ComplexRandomizer<Song>
{
    public SongRandomizer(IInstanceBuilder<Song> instanceBuilder) : base(instanceBuilder)
    {
        this.AddRandomizationRule(s => s.Id, new GuidRandomizer());
        this.AddRandomizationRule(s => s.Name, new StringRandomizer());
        this.AddRandomizationRule(s => s.Author, new StringRandomizer());
    }
}
