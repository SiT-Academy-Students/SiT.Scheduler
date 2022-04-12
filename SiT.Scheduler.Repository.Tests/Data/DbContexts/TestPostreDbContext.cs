namespace SiT.Scheduler.Repository.Tests.Data.DbContexts;

using Microsoft.EntityFrameworkCore;
using SiT.Scheduler.Repository.Tests.Data.Entities;

public class TestPostreDbContext : DbContext
{
    public TestPostreDbContext(DbContextOptions<TestPostreDbContext> options) 
        : base(options)
    {
    }

    public DbSet<TestEntity> TestEntities { get; set; }
}
