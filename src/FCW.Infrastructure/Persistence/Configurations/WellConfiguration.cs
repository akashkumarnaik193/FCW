using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FCW.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCW.Infrastructure.Persistence.Configurations;

public class WellConfiguration : IEntityTypeConfiguration<Well>
{
    public void Configure(EntityTypeBuilder<Well> builder)
    {
        builder.ToTable("Wells");

        builder.HasKey(w => w.Id);

        builder.Property(w => w.WellName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(w => w.Field)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(w => w.Country)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(w => w.Operator)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(w => w.CreatedBy)
            .IsRequired()
            .HasMaxLength(100);

        // A well name must be unique within a field - realistic business rule
        builder.HasIndex(w => new { w.WellName, w.Field }).IsUnique();

        builder.HasMany(w => w.DesignConcepts)
            .WithOne(d => d.Well)
            .HasForeignKey(d => d.WellId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}