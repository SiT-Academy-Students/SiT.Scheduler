namespace SiT.Scheduler.Repository.Tests.Integration;
using SiT.Scheduler.Data.Contracts.Repositories;
using SiT.Scheduler.Data.Models;
using SiT.Scheduler.Tests.Interface;
using Xunit.Abstractions;

public abstract class SongRepositoryTest<TDatabaseProvider> : BaseRepositoryTest<Song, TDatabaseProvider>
    where TDatabaseProvider : ITestDatabaseProvider
{
    protected SongRepositoryTest(TDatabaseProvider testDatabaseProvider, ITestOutputHelper testOutputHelper)
        : base(testDatabaseProvider, testOutputHelper)
    {
    }

    protected override IRepository<Song> Repository => this.SongRepository;

    protected override Song PreareEntity()
    {
        var song = new Song
        {
            Name = "First test's song",
            Author = "First test"
        };

        return song;
    }
}
