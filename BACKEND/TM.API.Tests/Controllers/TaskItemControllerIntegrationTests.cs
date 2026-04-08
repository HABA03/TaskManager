using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using FluentAssertions;
using TM.API.Tests.Fixtures;
using TM.Application.DTO.TaskItem.Create;
using TM.Application.DTO.TaskItem.GetAll;
using Xunit;

namespace TM.API.Tests.Controllers;

public class TaskItemControllerIntegrationTests : IClassFixture<TaskManagerWebApplicationFactory>
{
    private readonly HttpClient _client;

    public TaskItemControllerIntegrationTests(TaskManagerWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateTask_WithValidRequest_ShouldReturnSuccess()
    {
        var request = new CreateTaskRequest
        {
            Name = "Test",
            Description = "This is a valid test task description"
        };

        var response = await _client.PostAsJsonAsync("/TaskItem/Create", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GetAllTasks_ShouldReturnList()
    {
        var request = new CreateTaskRequest { Name = "Task", Description = "This is a description" };
        await _client.PostAsJsonAsync("/TaskItem/Create", request);

        var response = await _client.GetAsync("/TaskItem/GetAll");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task CreateAndRetrieveTask_ShouldWork()
    {
        var request = new CreateTaskRequest { Name = "Test", Description = "Full integration flow test task" };
        
        var createResponse = await _client.PostAsJsonAsync("/TaskItem/Create", request);
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var createContent = await createResponse.Content.ReadAsStringAsync();
        
        var getAllResponse = await _client.GetAsync("/TaskItem/GetAll");
        var content = await getAllResponse.Content.ReadAsStringAsync();
        
        // Debug: print content to see what we got
        Console.WriteLine($"Create response: {createContent}");
        Console.WriteLine($"GetAll response: {content}");
        
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain("Test");
    }
}
