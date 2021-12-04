namespace SiT.Scheduler.Tests.Fixtures.Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SiT.Scheduler.Data;
using SiT.Scheduler.Data.PostgreSql;
using Xunit;
using Xunit.Abstractions;

public class PostgreSqlTestDatabaseProvider : BaseTestDatabaseProvider
{
    protected override void SetupDbContextInternally(IServiceCollection serviceCollection, ITestOutputHelper testOutputHelper)
    {
        Assert.NotNull(serviceCollection);
        Assert.NotNull(testOutputHelper);

        serviceCollection.AddDbContext<SchedulerDbContext, PostgreSchedulerDbContext>(
            optionsBuilder =>
            {
                SetupDbContextOptions(optionsBuilder);
                AddLogging(optionsBuilder, testOutputHelper);
            });
    }

    protected override SchedulerDbContext InitializeDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<PostgreSchedulerDbContext>();
        SetupDbContextOptions(optionsBuilder);
        return new PostgreSchedulerDbContext(optionsBuilder.Options);
    }

    private static void SetupDbContextOptions(DbContextOptionsBuilder optionsBuilder)
    {
        Assert.NotNull(optionsBuilder);
        optionsBuilder.UseNpgsql("Host=localhost;Database=SchedulerTests;Username=scheduler_tests_user;Password=\"123456-Aa\"");
    }
}
