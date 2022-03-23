namespace SiT.Scheduler.Data;

using Microsoft.EntityFrameworkCore;
using SiT.Scheduler.Data.Models;

public class SchedulerDbContext : DbContext
{
    public SchedulerDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Genre> Genres { get; set; }

    public DbSet<Identity> Identities { get; set; }

    public DbSet<Song> Songs { get; set; }

    public DbSet<Performer> Performers { get; set;}

    public DbSet<Tenant> Tenants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Song>().HasMany(s => s.Performers).WithMany(p => p.Songs);
        modelBuilder.Entity<Performer>().HasMany(p => p.Songs).WithMany(s => s.Performers);

        modelBuilder.Entity<Song>().HasMany(s => s.Genres).WithMany(g => g.Songs);
        modelBuilder.Entity<Genre>().HasMany(g => g.Songs).WithMany(s => s.Genres);
        
        modelBuilder.Entity<Song>().HasMany(s => s.Categories).WithMany(c => c.Songs);
        modelBuilder.Entity<Category>().HasMany(c => c.Songs).WithMany(s => s.Categories);

        modelBuilder.Entity<Tenant>().HasMany(t => t.Identities).WithMany(i => i.Tenants);
        modelBuilder.Entity<Identity>().HasMany(i => i.Tenants).WithMany(t => t.Identities);

        modelBuilder.Entity<Tenant>().HasMany(t => t.Categories).WithOne(c => c.Tenant).HasForeignKey(c => c.TenantId);
        modelBuilder.Entity<Category>().HasOne(c => c.Tenant).WithMany(t => t.Categories).HasForeignKey(c => c.TenantId);
    }
}
