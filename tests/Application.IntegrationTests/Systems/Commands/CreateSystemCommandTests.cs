using System.Threading.Tasks;
using CleanArchitecture.Blazor.Application.Features.Systems.Commands.Create;
using CleanArchitecture.Blazor.Domain.Entities;
using FluentAssertions;
using FluentValidation;
using NUnit.Framework;

namespace CleanArchitecture.Blazor.Application.IntegrationTests.Systems.Commands;

using static Testing;

internal class CreateSystemCommandTests : TestBase
{
    [Test]
    public void ShouldThrowValidationException()
    {
        var command = new CreateSystemCommand();
        FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateSystem()
    {
        var command = new CreateSystemCommand
        {
            Name = "Test System",
            Description = "Test Description", 
            Category = "Application",
            Type = "Web",
            CotsCustom = "Custom",
            HostingCurrent = "On-Premise",
            HostingTarget = "Cloud",
            InsourceOutsource = "Insource",
            OwnerId = "test-user-id",
            Status = "Active"
        };

        var result = await SendAsync(command);
        var find = await FindAsync<Domain.Entities.System>(result.Data);
        
        find.Should().NotBeNull();
        find.Name.Should().Be("Test System");
        find.Description.Should().Be("Test Description");
        find.Category.Should().Be("Application");
        find.Type.Should().Be("Web");
        find.OwnerId.Should().Be("test-user-id");
        find.Status.Should().Be("Active");
    }

    [Test]
    public async Task ShouldRequireValidFields()
    {
        var command = new CreateSystemCommand
        {
            Name = "", // Empty name should fail validation
            Description = "", // Empty description should fail validation
        };

        FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }
}