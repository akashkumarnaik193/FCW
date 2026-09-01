using System;
using System.Collections.Generic;
using System.Text;

using FCW.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCW.Infrastructure.Persistence.Configurations;

public class WellEventConfiguration : IEntityTypeConfiguration<WellEvent>
{
    public void Configure(EntityTypeBuilder<WellEvent> builder)
    {
        builder.ToTable("WellEvents");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Notes).HasMaxLength(2000);
        builder.Property(e => e.MudType).HasMaxLength(100);
        builder.Property(e => e.CompletionType).HasMaxLength(100);
        builder.Property(e => e.InterventionReason).HasMaxLength(500);
        builder.Property(e => e.ToolUsed).HasMaxLength(200);
        builder.Property(e => e.AbandonmentReason).HasMaxLength(500);

        builder.Property(e => e.PlannedDepth).HasColumnType("decimal(10,2)");
        builder.Property(e => e.TubingSize).HasColumnType("decimal(10,2)");
        builder.Property(e => e.PlugDepth).HasColumnType("decimal(10,2)");

        builder.Property(e => e.CreatedBy).IsRequired().HasMaxLength(100);

        builder.HasOne(e => e.DesignConcept)
            .WithMany(d => d.WellEvents)
            .HasForeignKey(e => e.DesignConceptId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}