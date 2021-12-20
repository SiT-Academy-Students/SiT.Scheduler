namespace SiT.Scheduler.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SiT.Scheduler.Data;
using SiT.Scheduler.Data.PostgreSql;

public static class DatabaseConfigurationExtensions
{
    public static void SetupDatabase([NotNull] this IServiceCollection services, [NotNull] DatabaseConfiguration databaseConfiguration)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));
        if (databaseConfiguration is null) throw new ArgumentNullException(nameof(databaseConfiguration));

        if (databaseConfiguration.Provider == "postgre")
            services.SetupPostgreSql(databaseConfiguration.ConnectionString);
        else
            throw new InvalidOperationException("Invalid database provider.");
    }

    private static void SetupPostgreSql([NotNull] this IServiceCollection services, string connectionString)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));

        services.AddDbContext<SchedulerDbContext, PostgreSchedulerDbContext>(options => options.UseNpgsql(connectionString));
    }
}
