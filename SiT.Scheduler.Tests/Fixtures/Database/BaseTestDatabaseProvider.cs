namespace SiT.Scheduler.Tests.Fixtures.Database;

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SiT.Scheduler.Data;
using SiT.Scheduler.Tests.Interface;
using Xunit;
using Xunit.Abstractions;

public abstract class BaseTestDatabaseProvider : ITestDatabaseProvider, IAsyncLifetime
{
    private SchedulerDbContext _dbContext;

    public void SetupDbContext(IServiceCollection serviceCollection, ITestOutputHelper testOutputHelper)
    {
        Assert.NotNull(serviceCollection);
        Assert.NotNull(testOutputHelper);

        this.SetupDbContextInternally(serviceCollection, testOutputHelper);
    }

    public async Task InitializeAsync()
    {
        this._dbContext = this.InitializeDbContext();
        Assert.NotNull(this._dbContext);

        await this._dbContext.Database.MigrateAsync();
    }

    public Task DisposeAsync() => this._dbContext.Database.EnsureDeletedAsync();

    protected abstract void SetupDbContextInternally(IServiceCollection serviceCollection, ITestOutputHelper testOutputHelper);
    protected abstract SchedulerDbContext InitializeDbContext();

    protected static void AddLogging(DbContextOptionsBuilder optionsBuilder, ITestOutputHelper testOutputHelper)
    {
        Assert.NotNull(optionsBuilder);
        Assert.NotNull(testOutputHelper);

        // The `Information` logging level will render all queries sent to the database.
        optionsBuilder.LogTo(testOutputHelper.WriteLine, minimumLevel: LogLevel.Information).EnableSensitiveDataLogging();
    }
}
