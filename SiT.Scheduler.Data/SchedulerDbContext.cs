namespace SiT.Scheduler.Data;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using SiT.Scheduler.Data.Contracts.Models;
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

        modelBuilder.Entity<Identity>().HasIndex(i => i.ExternalId).IsUnique();

        DefineTenantRelationship(modelBuilder, t => t.Categories);
        DefineTenantRelationship(modelBuilder, t => t.Genres);
        DefineTenantRelationship(modelBuilder, t => t.Performers);
        DefineTenantRelationship(modelBuilder, t => t.Songs);
    }

    private static void DefineTenantRelationship<TEntity>([NotNull] ModelBuilder modelBuilder, Expression<Func<Tenant, IEnumerable<TEntity>>> navigationProperty)
        where TEntity : class, ITenantEntity
    {
        modelBuilder.Entity<Tenant>().HasMany(navigationProperty).WithOne(c => c.Tenant).HasForeignKey(c => c.TenantId);
        modelBuilder.Entity<TEntity>().HasOne(c => c.Tenant).WithMany(navigationProperty).HasForeignKey(c => c.TenantId);
    }
}
