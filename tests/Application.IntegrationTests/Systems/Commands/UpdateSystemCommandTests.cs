using System.Threading.Tasks;
using CleanArchitecture.Blazor.Application.Features.Systems.Commands.Create;
using CleanArchitecture.Blazor.Application.Features.Systems.Commands.Update;
using CleanArchitecture.Blazor.Domain.Entities;
using FluentAssertions;
using FluentValidation;
using NUnit.Framework;

namespace CleanArchitecture.Blazor.Application.IntegrationTests.Systems.Commands;

using static Testing;

internal class UpdateSystemCommandTests : TestBase
{
    [Test]
    public void ShouldThrowValidationException()
    {
        var command = new UpdateSystemCommand();
        FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldUpdateSystem()
    {
        // First create a system
        var createCommand = new CreateSystemCommand
        {
            Name = "Original System",
            Description = "Original Description",
            Category = "Application",
            Type = "Web",
            OwnerId = "test-user-id",
            Status = "Draft"
        };

        var createResult = await SendAsync(createCommand);
        var createdSystem = await FindAsync<Domain.Entities.System>(createResult.Data);

        // Then update it
        var updateCommand = new UpdateSystemCommand
        {
            Id = createdSystem.Id,
            Name = "Updated System",
            Description = "Updated Description",
            Category = "Database",
            Type = "Service",
            OwnerId = "test-user-id",
            Status = "Active",
            CotsCustom = "COTS",
            HostingCurrent = "Cloud",
            HostingTarget = "Hybrid"
        };

        await SendAsync(updateCommand);
        var updatedSystem = await FindAsync<Domain.Entities.System>(createdSystem.Id);

        updatedSystem.Should().NotBeNull();
        updatedSystem.Id.Should().Be(createdSystem.Id);
        updatedSystem.Name.Should().Be("Updated System");
        updatedSystem.Description.Should().Be("Updated Description");
        updatedSystem.Category.Should().Be("Database");
        updatedSystem.Type.Should().Be("Service");
        updatedSystem.Status.Should().Be("Active");
        updatedSystem.CotsCustom.Should().Be("COTS");
    }

    [Test]
    public async Task ShouldRequireValidId()
    {
        var command = new UpdateSystemCommand
        {
            Id = 0, // Invalid ID
            Name = "Test System",
            Description = "Test Description",
            Category = "Application",
            Type = "Web",
            OwnerId = "test-user-id"
        };

        FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }
}