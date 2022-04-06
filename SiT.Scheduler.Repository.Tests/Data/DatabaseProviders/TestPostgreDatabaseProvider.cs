namespace SiT.Scheduler.Repository.Tests.Data.DatabaseProviders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SiT.Scheduler.Repository.Tests.Data.DbContexts;
using SiT.Scheduler.Tests.Fixtures.Database;
using Xunit;
using Xunit.Abstractions;

public class TestPostgreDatabaseProvider : BaseTestDatabaseProvider
{
    protected override void SetupDbContextInternally(IServiceCollection serviceCollection, ITestOutputHelper testOutputHelper)
    {
        Assert.NotNull(serviceCollection);
        Assert.NotNull(testOutputHelper);

        serviceCollection.AddDbContext<DbContext, TestPostreDbContext>(
            optionsBuilder =>
            {
                SetupDbContextOptions(optionsBuilder);
                AddLogging(optionsBuilder, testOutputHelper);
            });
    }

    protected override DbContext InitializeDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<TestPostreDbContext>();
        SetupDbContextOptions(optionsBuilder);
        return new TestPostreDbContext(optionsBuilder.Options);
    }

    protected override bool IsRealDatabase => false;

    private static void SetupDbContextOptions(DbContextOptionsBuilder optionsBuilder)
    {
        Assert.NotNull(optionsBuilder);
        optionsBuilder.UseNpgsql("Host=localhost;Database=SchedulerTests;Username=scheduler_tests_user;Password=\"123456-Aa\"");
    }
}
