namespace SiT.Scheduler.Data
{
    using Microsoft.EntityFrameworkCore;
    using SiT.Scheduler.Data.Models;

    public class SchedulerDbContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }
    }
}
