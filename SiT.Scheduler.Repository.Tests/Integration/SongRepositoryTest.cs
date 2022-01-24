namespace SiT.Scheduler.Repository.Tests.Integration;
using SiT.Scheduler.Data.Contracts.Repositories;
using SiT.Scheduler.Data.Models;
using SiT.Scheduler.Tests.Interface;
using SiT.Scheduler.Tests.Randomization;
using Xunit.Abstractions;

public abstract class SongRepositoryTest<TDatabaseProvider> : BaseRepositoryTest<Song, TDatabaseProvider>
    where TDatabaseProvider : ITestDatabaseProvider
{
    protected SongRepositoryTest(TDatabaseProvider testDatabaseProvider, ITestOutputHelper testOutputHelper)
        : base(testDatabaseProvider, testOutputHelper)
    {
    }

    protected override IRepository<Song> Repository => this.SongRepository;

    protected override Song PrepareEntity()
    {
        var instaceBuilder = new SongInstanceBuilder();
        var randomizer = new SongRandomizer(instaceBuilder);

        return randomizer.PrepareRandomValue();
    }
}
