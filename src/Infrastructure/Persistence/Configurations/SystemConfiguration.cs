// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Blazor.Infrastructure.Persistence.Configurations;

#nullable disable
public class SystemConfiguration : IEntityTypeConfiguration<Domain.Entities.System>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.System> builder)
    {
        // Required fields as per AC 1 and validation requirements
        builder.Property(t => t.Name).HasMaxLength(255).IsRequired();
        builder.Property(t => t.Description).HasMaxLength(1000).IsRequired();
        builder.Property(t => t.Category).HasMaxLength(255).IsRequired();
        builder.Property(t => t.Type).HasMaxLength(255).IsRequired();
        builder.Property(t => t.OwnerId).HasMaxLength(255).IsRequired();
        
        // Optional fields with max lengths
        builder.Property(t => t.CotsCustom).HasMaxLength(255);
        builder.Property(t => t.HostingCurrent).HasMaxLength(255);
        builder.Property(t => t.HostingTarget).HasMaxLength(255);
        builder.Property(t => t.InsourceOutsource).HasMaxLength(255);
        builder.Property(t => t.Status).HasMaxLength(255);
        builder.Property(t => t.TenantId).HasMaxLength(255);
        
        // Relationships for hierarchical support (AC 6)
        builder.HasMany(s => s.Subsystems)
               .WithOne(ss => ss.System)
               .HasForeignKey(ss => ss.SystemId)
               .OnDelete(DeleteBehavior.Cascade);
        
        // Ignore domain events
        builder.Ignore(e => e.DomainEvents);
        
        // Indexes for performance
        builder.HasIndex(s => s.Name);
        builder.HasIndex(s => s.OwnerId);
        builder.HasIndex(s => s.Category);
        builder.HasIndex(s => s.Type);
        builder.HasIndex(s => s.TenantId);
    }
}

public class SubsystemConfiguration : IEntityTypeConfiguration<Subsystem>
{
    public void Configure(EntityTypeBuilder<Subsystem> builder)
    {
        builder.Property(t => t.Name).HasMaxLength(255).IsRequired();
        builder.Property(t => t.Type).HasMaxLength(255);
        
        // Foreign key relationship
        builder.HasOne(ss => ss.System)
               .WithMany(s => s.Subsystems)
               .HasForeignKey(ss => ss.SystemId)
               .OnDelete(DeleteBehavior.Cascade);
        
        // Ignore domain events
        builder.Ignore(e => e.DomainEvents);
        
        // Indexes
        builder.HasIndex(ss => ss.SystemId);
        builder.HasIndex(ss => ss.Name);
    }
}