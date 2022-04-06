namespace SiT.Scheduler.Core.Configuration;

using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SiT.Scheduler.Data;
using SiT.Scheduler.Data.Configuration;
using SiT.Scheduler.Data.PostgreSql;

public static class DatabaseConfigurationExtensions
{
    public static void SetupDatabase([NotNull] this IServiceCollection services, [NotNull] DatabaseConfiguration databaseConfiguration)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));
        if (databaseConfiguration is null) throw new ArgumentNullException(nameof(databaseConfiguration));

        if (string.Equals(databaseConfiguration.Provider, "postgre", StringComparison.InvariantCultureIgnoreCase)) services.SetupPostgreSql(databaseConfiguration.ConnectionString);
        else throw new InvalidOperationException("Invalid database provider.");
    }

    private static void SetupPostgreSql([NotNull] this IServiceCollection services, string connectionString)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));

        services.AddDbContext<DbContext, PostgreSchedulerDbContext>(options => options.UseNpgsql(connectionString));
    }
}
