namespace SiT.Scheduler.Data.PostgreSql;

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class DbContextFactory : IDesignTimeDbContextFactory<PostgreSchedulerDbContext>
{
    public PostgreSchedulerDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PostgreSchedulerDbContext>();
        if (args is null || !args.Any() || string.IsNullOrWhiteSpace(args[0]))
            throw new InvalidOperationException("Please provide a valid connection string.");

        optionsBuilder.UseNpgsql(args[0]);
        return new PostgreSchedulerDbContext(optionsBuilder.Options);
    }
}
