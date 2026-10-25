using System.Threading.Tasks;
using CleanArchitecture.Blazor.Application.Features.Systems.Commands.Create;
using CleanArchitecture.Blazor.Application.Features.Systems.Queries.Pagination;
using CleanArchitecture.Blazor.Application.Features.Systems.Specifications;
using FluentAssertions;
using NUnit.Framework;

namespace CleanArchitecture.Blazor.Application.IntegrationTests.Systems.Queries;

using static Testing;

internal class SystemsWithPaginationQueryTests : TestBase
{
    [Test]
    public async Task ShouldReturnSystemsList()
    {
        // Create test systems
        var createCommand1 = new CreateSystemCommand
        {
            Name = "Test System 1",
            Description = "First test system",
            Category = "Application",
            Type = "Web",
            OwnerId = "test-user-id",
            Status = "Active"
        };

        var createCommand2 = new CreateSystemCommand
        {
            Name = "Test System 2",
            Description = "Second test system", 
            Category = "Database",
            Type = "Service",
            OwnerId = "test-user-id",
            Status = "Draft"
        };

        await SendAsync(createCommand1);
        await SendAsync(createCommand2);

        var query = new SystemsWithPaginationQuery
        {
            ListView = SystemListView.All,
            PageNumber = 1,
            PageSize = 10,
            Keyword = ""
        };

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.TotalItems.Should().BeGreaterThanOrEqualTo(2);
        result.Items.Should().NotBeEmpty();
        result.Items.Should().Contain(s => s.Name == "Test System 1");
        result.Items.Should().Contain(s => s.Name == "Test System 2");
    }

    [Test]
    public async Task ShouldFilterSystemsByKeyword()
    {
        // Create test systems
        var createCommand = new CreateSystemCommand
        {
            Name = "Unique Filter Test System",
            Description = "System for filter testing",
            Category = "Application",
            Type = "Web",
            OwnerId = "test-user-id",
            Status = "Active"
        };

        await SendAsync(createCommand);

        var query = new SystemsWithPaginationQuery
        {
            ListView = SystemListView.All,
            PageNumber = 1,
            PageSize = 10,
            Keyword = "Unique Filter"
        };

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.Items.Should().NotBeEmpty();
        result.Items.Should().Contain(s => s.Name.Contains("Unique Filter"));
    }

    [Test]
    public async Task ShouldReturnEmptyResultForNoMatches()
    {
        var query = new SystemsWithPaginationQuery
        {
            ListView = SystemListView.All,
            PageNumber = 1,
            PageSize = 10,
            Keyword = "NonExistentSystemKeyword12345"
        };

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.TotalItems.Should().Be(0);
        result.Items.Should().BeEmpty();
    }
}