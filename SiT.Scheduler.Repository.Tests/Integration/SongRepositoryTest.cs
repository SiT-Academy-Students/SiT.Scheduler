namespace SiT.Scheduler.Repository.Tests.Integration;

using SiT.Scheduler.Data.Contracts.Repositories;
using SiT.Scheduler.Data.Models;
using SiT.Scheduler.Tests.Interface;
using SiT.Scheduler.Tests.Randomization;
using SiT.Scheduler.Utilities;
using TryAtSoftware.Randomizer.Core.Interfaces;
using Xunit.Abstractions;

public abstract class SongRepositoryTest<TDatabaseProvider> : BaseRepositoryTest<Song, TDatabaseProvider>
    where TDatabaseProvider : ITestDatabaseProvider
{
    protected SongRepositoryTest(TDatabaseProvider testDatabaseProvider, ITestOutputHelper testOutputHelper)
        : base(testDatabaseProvider, testOutputHelper)
    {
    }

    protected override IRepository<Song> Repository => this.SongRepository;

    protected override Song PrepareEntity(params IRandomizationRule<Song>[] overridenRules)
    {
        var instaceBuilder = new SongInstanceBuilder();
        var randomizer = new SongRandomizer(instaceBuilder);

        foreach (var overridenRule in overridenRules.OrEmptyIfNull().IgnoreNullValues())
            randomizer.OverrideRandomizationRule(overridenRule);

        return randomizer.PrepareRandomValue();
    }
}
