namespace SiT.Scheduler.Repository.Tests.Integration;

using System;
using System.Threading.Tasks;
using SiT.Scheduler.Data.Models;
using SiT.Scheduler.Tests;
using SiT.Scheduler.Tests.Interface;
using Xunit;
using Xunit.Abstractions;

public abstract class BaseRepositoryTest<TDatabaseProvider> : BaseIntegrationTest<TDatabaseProvider>
    where TDatabaseProvider : ITestDatabaseProvider
{
    protected BaseRepositoryTest(TDatabaseProvider testDatabaseProvider, ITestOutputHelper testOutputHelper)
        : base(testDatabaseProvider, testOutputHelper)
    {
    }

    [Fact]
    public async Task CreateSongShouldWorkCorrectly()
    {
        var song = new Song
        {
            Id = Guid.NewGuid(),
            Name = "song",
            Author = "George Michael"
        };
        var createSong = await this.SongRepository.CreateAsync(song, this.GetCancellationToken());
        Assert.NotNull(createSong);
        Assert.True(createSong.IsSuccessful, createSong.ToString());
    }
}
