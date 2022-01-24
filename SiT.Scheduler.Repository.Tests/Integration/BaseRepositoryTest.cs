namespace SiT.Scheduler.Repository.Tests.Integration;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SiT.Scheduler.Data.Contracts.Models;
using SiT.Scheduler.Data.Contracts.Repositories;
using SiT.Scheduler.Tests;
using SiT.Scheduler.Tests.Interface;
using SiT.Scheduler.Utilities;
using TryAtSoftware.Randomizer.Core;
using TryAtSoftware.Randomizer.Core.Interfaces;
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

    [Fact]
    public async Task UpdateShouldWorkCorrectly()
    {
        var originalEntity = this.PrepareEntity();
        var createEntity = await this.Repository.CreateAsync(originalEntity, this.GetCancellationToken());
        Assert.NotNull(createEntity);
        Assert.True(createEntity.IsSuccessful, createEntity.ToString());

        var overridenIdRandomizationRule = new RandomizationRule<TEntity, Guid>(t => t.Id, originalEntity.Id.AsConstantRandomizer());
        var updatedEntity = this.PrepareEntity(overridenIdRandomizationRule);
        var updateEntity = await this.Repository.UpdateAsync(updatedEntity, this.GetCancellationToken());
        Assert.NotNull(updateEntity);
        Assert.True(updateEntity.IsSuccessful, updateEntity.ToString());

        Expression<Func<TEntity, bool>> idFilter = x => x.Id == originalEntity.Id;
        var getEntity = await this.Repository.GetAsync(idFilter.AsEnumerable(), this.GetCancellationToken());
        Assert.NotNull(getEntity);
        Assert.True(getEntity.IsSuccessful, getEntity.ToString());

        this.Equalizer.AssertEquality(updatedEntity, getEntity.Data);
    }

    protected abstract IRepository<TEntity> Repository { get; }
    protected abstract TEntity PrepareEntity(params IRandomizationRule<TEntity>[] overridenRules);
}
