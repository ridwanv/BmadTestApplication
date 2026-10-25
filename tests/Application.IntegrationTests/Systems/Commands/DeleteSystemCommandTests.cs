using System.Threading.Tasks;
using CleanArchitecture.Blazor.Application.Features.Systems.Commands.Create;
using CleanArchitecture.Blazor.Application.Features.Systems.Commands.Delete;
using CleanArchitecture.Blazor.Domain.Entities;
using FluentAssertions;
using FluentValidation;
using NUnit.Framework;

namespace CleanArchitecture.Blazor.Application.IntegrationTests.Systems.Commands;

using static Testing;

internal class DeleteSystemCommandTests : TestBase
{
    [Test]
    public void ShouldThrowValidationException()
    {
        var command = new DeleteSystemCommand(new int[] { }); // Empty array should fail
        FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldDeleteSystem()
    {
        // First create a system
        var createCommand = new CreateSystemCommand
        {
            Name = "System to Delete",
            Description = "This will be deleted",
            Category = "Application",
            Type = "Web",
            OwnerId = "test-user-id",
            Status = "Draft"
        };

        var createResult = await SendAsync(createCommand);
        var createdSystem = await FindAsync<Domain.Entities.System>(createResult.Data);
        createdSystem.Should().NotBeNull();

        // Then delete it
        var deleteCommand = new DeleteSystemCommand(new int[] { createdSystem.Id });
        await SendAsync(deleteCommand);

        var deletedSystem = await FindAsync<Domain.Entities.System>(createdSystem.Id);
        deletedSystem.Should().BeNull();
    }

    [Test]
    public async Task ShouldDeleteMultipleSystems()
    {
        // Create multiple systems
        var createCommand1 = new CreateSystemCommand
        {
            Name = "System 1",
            Description = "First system",
            Category = "Application",
            Type = "Web",
            OwnerId = "test-user-id",
            Status = "Draft"
        };

        var createCommand2 = new CreateSystemCommand
        {
            Name = "System 2", 
            Description = "Second system",
            Category = "Database",
            Type = "Service",
            OwnerId = "test-user-id",
            Status = "Active"
        };

        var result1 = await SendAsync(createCommand1);
        var result2 = await SendAsync(createCommand2);

        // Delete both systems
        var deleteCommand = new DeleteSystemCommand(new int[] { result1.Data, result2.Data });
        await SendAsync(deleteCommand);

        var deletedSystem1 = await FindAsync<Domain.Entities.System>(result1.Data);
        var deletedSystem2 = await FindAsync<Domain.Entities.System>(result2.Data);
        
        deletedSystem1.Should().BeNull();
        deletedSystem2.Should().BeNull();
    }

    [Test]
    public void ShouldRequireValidIds()
    {
        var command = new DeleteSystemCommand(new int[] { 0, -1 }); // Invalid IDs
        FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }
}