using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiT.Scheduler.Data.PostgreSql
{
    class DbContextFactory : IDesignTimeDbContextFactory<PostgreDbContext>
    {
        public PostgreDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PostgreDbContext>();
            if (args is null || string.IsNullOrWhiteSpace(args[0]) || args.Length < 1)
                optionsBuilder.UseNpgsql(args[0]);
            else
                throw new InvalidOperationException("Please provide a valid connection string.");

            return new PostgreDbContext(optionsBuilder.Options);
        }
    }
}
