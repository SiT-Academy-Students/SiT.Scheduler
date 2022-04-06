namespace SiT.Scheduler.Repository.Tests.Data.Entities;
using TryAtSoftware.Randomizer.Core;
using TryAtSoftware.Randomizer.Core.Interfaces;
using TryAtSoftware.Randomizer.Core.Primitives;

public class TestEntityRandomizer : ComplexRandomizer<TestEntity>
{
    public TestEntityRandomizer(IInstanceBuilder<TestEntity> instanceBuilder) : base(instanceBuilder)
    {
        this.AddRandomizationRule(te => te.Id, new GuidRandomizer());
        this.AddRandomizationRule(te => te.RandomString, new StringRandomizer());
        this.AddRandomizationRule(te => te.RandomNumber, new NumberRandomizer());
        this.AddRandomizationRule(te => te.RandomBoolean, new BooleanRandomizer());
    }
}
