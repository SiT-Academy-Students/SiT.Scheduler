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

    public DbSet<Performer> Performers { get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Song>()
            .HasMany(s => s.Performers)
            .WithMany(p => p.Songs);

        modelBuilder.Entity<Performer>()
            .HasMany(p => p.Songs)
            .WithMany(s => s.Performers);
    }  
}
