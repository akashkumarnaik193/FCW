using FCW.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FCW.Infrastructure.Persistence;

public class FcwDbContext : DbContext
{
    public FcwDbContext(DbContextOptions<FcwDbContext> options) : base(options)
    {
    }

    public DbSet<Well> Wells => Set<Well>();
    public DbSet<DesignConcept> DesignConcepts => Set<DesignConcept>();
    public DbSet<WellEvent> WellEvents => Set<WellEvent>();
    public DbSet<Casing> Casings => Set<Casing>();
    public DbSet<UpperCompletion> UpperCompletions => Set<UpperCompletion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FcwDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}