using System;
using System.Collections.Generic;
using System.Text;

using FCW.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCW.Infrastructure.Persistence.Configurations;

public class UpperCompletionConfiguration : IEntityTypeConfiguration<UpperCompletion>
{
    public void Configure(EntityTypeBuilder<UpperCompletion> builder)
    {
        builder.ToTable("UpperCompletions");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.ComponentConfiguration).HasMaxLength(500);
        builder.Property(u => u.TubingType).HasMaxLength(100);
        builder.Property(u => u.PackerType).HasMaxLength(100);
        builder.Property(u => u.CreatedBy).IsRequired().HasMaxLength(100);
        builder.Property(u => u.TubingLength).HasColumnType("decimal(10,2)");

        builder.HasOne(u => u.WellEvent)
            .WithOne(e => e.UpperCompletion)
            .HasForeignKey<UpperCompletion>(u => u.WellEventId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(u => u.WellEventId).IsUnique();
    }
}