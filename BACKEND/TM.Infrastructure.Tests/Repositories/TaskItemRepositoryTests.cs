using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TM.Domain.Entity;
using TM.Domain.Enum;
using TM.Infrastructure.Context.TMContext;
using TM.Infrastructure.Repositories.TaskItemRepository;
using Xunit;

namespace TM.Infrastructure.Tests.Repositories;

public class TaskItemRepositoryTests
{
    private readonly TMContext _context;
    private readonly TaskItemRepository _repository;

    public TaskItemRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<TMContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new TMContext(options);
        _repository = new TaskItemRepository(_context);
    }

    [Fact]
    public async Task CreateTaskItem_WithValidData_ShouldReturnTrue()
    {
        var taskItem = new TaskItem
        {
            Name = "Test Task",
            Description = "Test Description",
            Status = TaskStatusEnum.Active
        };
        var cancellationToken = CancellationToken.None;

        var result = await _repository.CreateTaskItem(taskItem, cancellationToken);

        result.Should().BeTrue();
        _context.TaskItem.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetAllTaskItem_ShouldReturnAllTasks()
    {
        var tasks = new List<TaskItem>
        {
            new() { Name = "Task 1", Description = "Desc 1", Status = TaskStatusEnum.Active },
            new() { Name = "Task 2", Description = "Desc 2", Status = TaskStatusEnum.Pending }
        };
        await _context.TaskItem.AddRangeAsync(tasks);
        await _context.SaveChangesAsync();

        var result = await _repository.GetAllTaskItem(CancellationToken.None);

        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetTaskItemById_WithValidId_ShouldReturnTask()
    {
        var task = new TaskItem { Name = "Test Task", Description = "Desc", Status = TaskStatusEnum.Active };
        _context.TaskItem.Add(task);
        await _context.SaveChangesAsync();

        var result = await _repository.GetTaskItemById(task.Id, CancellationToken.None);

        result.Should().NotBeNull();
        result.Name.Should().Be("Test Task");
    }

    [Fact]
    public async Task UpdateTaskItem_WithValidData_ShouldReturnTrue()
    {
        var task = new TaskItem { Name = "Original", Description = "Desc", Status = TaskStatusEnum.Active };
        _context.TaskItem.Add(task);
        await _context.SaveChangesAsync();

        task.Name = "Updated";
        var result = await _repository.UpdateTaskItem(task, CancellationToken.None);

        result.Should().BeTrue();
        var updated = _context.TaskItem.First();
        updated.Name.Should().Be("Updated");
    }

    [Fact]
    public async Task RemoveTaskItem_WithValidId_ShouldReturnTrue()
    {
        var task = new TaskItem { Name = "Task", Description = "Desc", Status = TaskStatusEnum.Active };
        _context.TaskItem.Add(task);
        await _context.SaveChangesAsync();

        var result = await _repository.RemoveTaskItem(task.Id, CancellationToken.None);

        result.Should().BeTrue();
        _context.TaskItem.Should().HaveCount(0);
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}
