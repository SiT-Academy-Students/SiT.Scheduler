namespace SiT.Scheduler.Repository.Tests.Integration;
using System.Threading.Tasks;
using SiT.Scheduler.Data.Contracts.Models;
using SiT.Scheduler.Data.Contracts.Repositories;
using SiT.Scheduler.Tests;
using SiT.Scheduler.Tests.Interface;
using Xunit;
using Xunit.Abstractions;

public abstract class BaseRepositoryTest<TEntity, TDatabaseProvider> : BaseIntegrationTest<TDatabaseProvider>
    where TEntity : class, IEntity
    where TDatabaseProvider : ITestDatabaseProvider
{
    protected BaseRepositoryTest(TDatabaseProvider testDatabaseProvider, ITestOutputHelper testOutputHelper)
        : base(testDatabaseProvider, testOutputHelper)
    {
    }

    [Fact]
    public async Task CreateSongShouldWorkCorrectly()
    {
        var entity = this.PreareEntity();
        var createSong = await this.Repository.CreateAsync(entity, this.GetCancellationToken());
        Assert.NotNull(createSong);
        Assert.True(createSong.IsSuccessful, createSong.ToString());
    }

    protected abstract IRepository<TEntity> Repository { get; }
    protected abstract TEntity PreareEntity();
}
