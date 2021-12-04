namespace SiT.Scheduler.Data.PostgreSql;

using Microsoft.EntityFrameworkCore;

public class PostgreSchedulerDbContext : SchedulerDbContext
{
    public PostgreSchedulerDbContext(DbContextOptions<PostgreSchedulerDbContext> options)
        : base(options)
    {
    }
}
