namespace SiT.Scheduler.Data;

using Microsoft.EntityFrameworkCore;
using SiT.Scheduler.Data.Models;

public class SchedulerDbContext : DbContext
{
    public SchedulerDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Song> Songs { get; set; }
}
