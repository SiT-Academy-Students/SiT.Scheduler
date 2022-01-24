namespace SiT.Scheduler.Repository.Tests.Integration;

using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SiT.Scheduler.Data.Contracts.Models;
using SiT.Scheduler.Data.Contracts.Repositories;
using SiT.Scheduler.Tests;
using SiT.Scheduler.Tests.Interface;
using SiT.Scheduler.Utilities;
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
    public async Task CreateShouldWorkCorrectly()
    {
        var entity = this.PrepareEntity();
        var createEntity = await this.Repository.CreateAsync(entity, this.GetCancellationToken());
        Assert.NotNull(createEntity);
        Assert.True(createEntity.IsSuccessful, createEntity.ToString());

        Expression<Func<TEntity, bool>> idFilter = x => x.Id == entity.Id;
        var getEntity = await this.Repository.GetAsync(idFilter.AsEnumerable(), this.GetCancellationToken());
        Assert.NotNull(getEntity);
        Assert.True(getEntity.IsSuccessful, getEntity.ToString());

        this.Equalizer.AssertEquality(entity, getEntity.Data);
    }

    protected abstract IRepository<TEntity> Repository { get; }
    protected abstract TEntity PrepareEntity();
}
