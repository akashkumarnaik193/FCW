using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FCW.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCW.Infrastructure.Persistence.Configurations;

public class DesignConceptConfiguration : IEntityTypeConfiguration<DesignConcept>
{
    public void Configure(EntityTypeBuilder<DesignConcept> builder)
    {
        builder.ToTable("DesignConcepts");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(d => d.Description)
            .HasMaxLength(1000);

        builder.Property(d => d.CreatedBy)
            .IsRequired()
            .HasMaxLength(100);
    }
}
