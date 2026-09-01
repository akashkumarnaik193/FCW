using System;
using System.Collections.Generic;
using System.Text;

using FCW.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCW.Infrastructure.Persistence.Configurations;

public class CasingConfiguration : IEntityTypeConfiguration<Casing>
{
    public void Configure(EntityTypeBuilder<Casing> builder)
    {
        builder.ToTable("Casings");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.CasingType).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Grade).HasMaxLength(50);
        builder.Property(c => c.Material).HasMaxLength(100);
        builder.Property(c => c.Connection).HasMaxLength(100);
        builder.Property(c => c.Status).HasMaxLength(50);
        builder.Property(c => c.CreatedBy).IsRequired().HasMaxLength(100);

        builder.Property(c => c.Diameter).HasColumnType("decimal(10,2)");
        builder.Property(c => c.Weight).HasColumnType("decimal(10,2)");
        builder.Property(c => c.Depth).HasColumnType("decimal(10,2)");

        builder.HasOne(c => c.WellEvent)
            .WithMany(e => e.Casings)
            .HasForeignKey(c => c.WellEventId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}