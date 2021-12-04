namespace SiT.Scheduler.Tests;

using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using SiT.Scheduler.Core.Configuration;
using SiT.Scheduler.Data.Contracts.Repositories;
using SiT.Scheduler.Data.Models;
using SiT.Scheduler.Tests.Interface;
using Xunit;
using Xunit.Abstractions;

public abstract class BaseIntegrationTest<TDatabaseProvider> : IDisposable
    where TDatabaseProvider : ITestDatabaseProvider
{
    private readonly ServiceProvider _rootServiceProvider;
    private readonly IServiceScope _serviceScope;
    private bool _disposedValue;

    protected ITestOutputHelper TestOutputHelper { get; }
    protected IServiceProvider ServiceProvider { get; }

    protected IRepository<Song> SongRepository => this.GetService<IRepository<Song>>();

    protected BaseIntegrationTest(TDatabaseProvider testDatabaseProvider, ITestOutputHelper testOutputHelper)
    {
        Assert.NotNull(testDatabaseProvider);
        Assert.NotNull(testOutputHelper);
        this.TestOutputHelper = testOutputHelper;

        var serviceCollection = new ServiceCollection();
        testDatabaseProvider.SetupDbContext(serviceCollection, testOutputHelper);
        serviceCollection.RegisterServices();
        this._rootServiceProvider = serviceCollection.BuildServiceProvider();
        this._serviceScope = this._rootServiceProvider.CreateScope();
        this.ServiceProvider = this._serviceScope.ServiceProvider;
    }

    protected CancellationToken GetCancellationToken() => CancellationToken.None;

    private TService GetService<TService>()
        where TService : class
    {
        var service = this.ServiceProvider.GetService<TService>();
        Assert.NotNull(service);

        return service;
    }

    public void Dispose()
    {
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (this._disposedValue)
            return;

        if (disposing)
        {
            this._serviceScope.Dispose();
            this._rootServiceProvider.Dispose();
        }

        this._disposedValue = true;
    }
}
