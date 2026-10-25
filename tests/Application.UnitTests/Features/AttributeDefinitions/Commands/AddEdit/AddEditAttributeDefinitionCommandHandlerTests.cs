using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using CleanArchitecture.Blazor.Application.Common.Interfaces;
using CleanArchitecture.Blazor.Domain.Common;
using CleanArchitecture.Blazor.Domain.Identity;
using CleanArchitecture.Blazor.Application.Features.AttributeDefinitions.Commands.AddEdit;
using CleanArchitecture.Blazor.Domain.Entities;
using CleanArchitecture.Blazor.Infrastructure.Persistence;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;

namespace Application.UnitTests.Features.AttributeDefinitions.Commands.AddEdit
{
    /// <summary>
    /// QA Results:
    /// Status: CONCERNS
    /// Review Date: 2025-08-25
    /// 
    /// Key Findings:
    /// 1. Test Coverage Gaps:
    ///    - Missing update scenarios
    ///    - No validation testing
    ///    - Limited error cases
    /// 
    /// 2. Infrastructure Issues:
    ///    - EF Core configuration needs improvement
    ///    - Audit tracking verification missing
    ///    - Test context cleanup required
    /// 
    /// Recommendations:
    /// - Add comprehensive test cases
    /// - Implement TestBase class
    /// - Add audit field validation
    /// - Improve context configuration
    /// 
    /// See detailed review in qa/gates directory.
    /// </summary>
    public class AddEditAttributeDefinitionCommandHandlerTests
    {
        [Fact]
        public async Task Should_Create_AttributeDefinition()
        {
            // Create a new service collection
            var services = new ServiceCollection();
            
            // Add base EF Core services
            services.AddEntityFrameworkInMemoryDatabase();

            // Register DbContext
            services.AddDbContext<TestDbContext>((sp, options) =>
            {
                options.UseInMemoryDatabase("AttributeDefinitionTestDb")
                      .EnableSensitiveDataLogging()
                      .EnableDetailedErrors();
            });

            // Build service provider
            var serviceProvider = services.BuildServiceProvider();

            // Get context instance from service provider
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TestDbContext>();

            var handler = new AddEditAttributeDefinitionCommandHandler(context);
            var command = new AddEditAttributeDefinitionCommand
            {
                Name = "TestAttribute",
                DataType = "string",
                Mandatory = true,
                AllowedValues = new System.Collections.Generic.List<string> { "A", "B" },
                Regex = "^[A-B]$"
            };

            // Execute test
            var id = await handler.Handle(command, CancellationToken.None);
            id.Should().BeGreaterThan(0);

            // Verify results
            var entity = await context.AttributeDefinitions.FindAsync(id);
            entity.Should().NotBeNull();
            entity!.Name.Should().Be("TestAttribute");
        }
    }

    public class TestDbContext : DbContext, IApplicationDbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }

        public DbSet<AttributeDefinition> AttributeDefinitions { get; set; } = null!;
        
        // Implement the rest of the interface with null implementations since we don't use them in this test
        public DbSet<AuditTrail> AuditTrails { get; set; } = null!;
        public DbSet<SystemLog> SystemLogs { get; set; } = null!;
        public DbSet<Document> Documents { get; set; } = null!;
        public DbSet<PicklistSet> PicklistSets { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Tenant> Tenants { get; set; } = null!;
        public DbSet<Contact> Contacts { get; set; } = null!;
        public DbSet<CleanArchitecture.Blazor.Domain.Entities.System> Systems { get; set; } = null!;
        public DbSet<Subsystem> Subsystems { get; set; } = null!;
        public DbSet<LoginAudit> LoginAudits { get; set; } = null!;
        public DbSet<UserLoginRiskSummary> UserLoginRiskSummaries { get; set; } = null!;
        public DbSet<Microsoft.AspNetCore.DataProtection.EntityFrameworkCore.DataProtectionKey> DataProtectionKeys { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure AttributeDefinition
            modelBuilder.Entity<AttributeDefinition>(builder =>
            {
                builder.ToTable("AttributeDefinitions");
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Name).HasMaxLength(128).IsRequired();
                builder.Property(x => x.DataType).HasMaxLength(50).IsRequired();
                
                builder.Property(e => e.AllowedValues)
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                        v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null) ?? new List<string>()
                    );

                builder.Ignore(x => x.DomainEvents);
            });

            // Configure audit properties common to all entities
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IAuditableEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType).Property("Created").HasColumnType("datetime");
                    modelBuilder.Entity(entityType.ClrType).Property("LastModified").HasColumnType("datetime");
                    modelBuilder.Entity(entityType.ClrType).Property("CreatedBy").HasMaxLength(128);
                    modelBuilder.Entity(entityType.ClrType).Property("LastModifiedBy").HasMaxLength(128);
                }
            }
        }
    }
}
