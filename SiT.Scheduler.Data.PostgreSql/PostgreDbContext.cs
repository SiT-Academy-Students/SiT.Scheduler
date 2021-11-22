namespace SiT.Scheduler.Data.PostgreSql
{
    using Microsoft.EntityFrameworkCore;

    public class PostgreDbContext : SchedulerDbContext
    {
        public PostgreDbContext(DbContextOptions<PostgreDbContext> options)
        {
        }
    }
}