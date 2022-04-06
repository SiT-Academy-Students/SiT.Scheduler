namespace SiT.Scheduler.Repository.Tests.Integration;

using SiT.Scheduler.Data.Contracts.Repositories;
using SiT.Scheduler.Repository.Tests.Data.Entities;
using SiT.Scheduler.Tests.Interface;
using SiT.Scheduler.Utilities;
using TryAtSoftware.Randomizer.Core;
using TryAtSoftware.Randomizer.Core.Interfaces;
using Xunit.Abstractions;

public abstract class GeneralRepositoryTest<TDatabaseProvider> : BaseRepositoryTest<TestEntity, TDatabaseProvider>
    where TDatabaseProvider : ITestDatabaseProvider
{
    protected GeneralRepositoryTest(TDatabaseProvider testDatabaseProvider, ITestOutputHelper testOutputHelper)
        : base(testDatabaseProvider, testOutputHelper)
    {
    }

    protected override TestEntity PrepareEntity(params IRandomizationRule<TestEntity>[] overridenRules)
    {
        var instaceBuilder = new GeneralInstanceBuilder<TestEntity>();
        var randomizer = new TestEntityRandomizer(instaceBuilder);

        foreach (var overridenRule in overridenRules.OrEmptyIfNull().IgnoreNullValues())
            randomizer.OverrideRandomizationRule(overridenRule);

        return randomizer.PrepareRandomValue();
    }
}
