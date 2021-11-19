namespace SiT.Scheduler.Data.PostgreSql
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    public class DbContextFactory : IDesignTimeDbContextFactory<PostgreDbContext>
    {
        public PostgreDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PostgreDbContext>();
            if (string.IsNullOrWhiteSpace(args[0]) || args.Length < 1 || args is null)
                throw new InvalidOperationException("Please provide a valid connection string.");

            optionsBuilder.UseNpgsql(args[0]);


            return new PostgreDbContext(optionsBuilder.Options);
        }
    }
}