using FCW.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FCW.Infrastructure.Persistence;

public class FcwDbContext : DbContext
{
    public FcwDbContext(DbContextOptions<FcwDbContext> options) : base(options)
    {
    }

    public DbSet<Well> Wells => Set<Well>();
    public DbSet<DesignConcept> DesignConcepts => Set<DesignConcept>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FcwDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
