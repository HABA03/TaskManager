using FluentAssertions;
using TM.Domain.Entity;
using TM.Domain.Enum;
using Xunit;

namespace TM.Domain.Tests.Entity;

public class TaskItemTests
{
    [Fact]
    public void TaskItem_WhenCreated_ShouldHaveDefaultValues()
    {
        var taskItem = new TaskItem();

        taskItem.Id.Should().Be(0);
        taskItem.Name.Should().Be(string.Empty);
        taskItem.Description.Should().Be(string.Empty);
    }

    [Fact]
    public void TaskItem_WhenInitializedWithValues_ShouldContainThemCorrectly()
    {
        var taskItem = new TaskItem
        {
            Id = 1,
            Name = "Test Task",
            Description = "Test Description",
            Status = TaskStatusEnum.Active
        };

        taskItem.Id.Should().Be(1);
        taskItem.Name.Should().Be("Test Task");
        taskItem.Description.Should().Be("Test Description");
        taskItem.Status.Should().Be(TaskStatusEnum.Active);
    }

    [Theory]
    [InlineData(TaskStatusEnum.Active)]
    [InlineData(TaskStatusEnum.Pending)]
    [InlineData(TaskStatusEnum.Deleted)]
    public void TaskItem_CanChangeStatus(TaskStatusEnum status)
    {
        var taskItem = new TaskItem { Status = status };

        taskItem.Status.Should().Be(status);
    }
}
