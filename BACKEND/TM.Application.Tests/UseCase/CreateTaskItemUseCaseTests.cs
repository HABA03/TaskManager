using AutoMapper;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using TM.Application.DTO.TaskItem.Create;
using TM.Application.UseCase.TaskItemUseCases;
using TM.Domain.Entity;
using TM.Domain.Interface;
using Xunit;

namespace TM.Application.Tests.UseCase;

public class CreateTaskItemUseCaseTests
{
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ITaskItemRepository> _mockRepository;
    private readonly Mock<IValidator<CreateTaskRequest>> _mockValidator;
    private readonly CreateTaskItemUseCase _useCase;

    public CreateTaskItemUseCaseTests()
    {
        _mockMapper = new Mock<IMapper>();
        _mockRepository = new Mock<ITaskItemRepository>();
        _mockValidator = new Mock<IValidator<CreateTaskRequest>>();
        _useCase = new CreateTaskItemUseCase(_mockMapper.Object, _mockRepository.Object, _mockValidator.Object);
    }

    [Fact]
    public async Task CreateTask_WithValidRequest_ShouldReturnTrue()
    {
        var request = new CreateTaskRequest { Name = "Test Task", Description = "Valid Description" };
        var taskItem = new TaskItem { Name = "Test Task", Description = "Valid Description" };
        var cancellationToken = CancellationToken.None;

        _mockValidator.Setup(v => v.ValidateAsync(request, cancellationToken))
            .ReturnsAsync(new ValidationResult());

        _mockMapper.Setup(m => m.Map<TaskItem>(request)).Returns(taskItem);
        _mockRepository.Setup(r => r.CreateTaskItem(taskItem, cancellationToken)).ReturnsAsync(true);

        var result = await _useCase.Handle(request, cancellationToken);

        result.Response.Should().BeTrue();
        result.Message.Should().Contain("created");
    }

    [Fact]
    public async Task CreateTask_WithValidationError_ShouldReturnFalse()
    {
        var request = new CreateTaskRequest { Name = "", Description = "" };
        var cancellationToken = CancellationToken.None;
        var validationErrors = new List<ValidationFailure>
        {
            new("Name", "Name is required")
        };
        var validationResult = new ValidationResult(validationErrors);

        _mockValidator.Setup(v => v.ValidateAsync(request, cancellationToken))
            .ReturnsAsync(validationResult);

        var result = await _useCase.Handle(request, cancellationToken);

        result.Response.Should().BeFalse();
        result.Message.Should().Contain("Name is required");
    }

    [Fact]
    public async Task CreateTask_WhenRepositoryFails_ShouldReturnFalse()
    {
        var request = new CreateTaskRequest { Name = "Task", Description = "Description" };
        var taskItem = new TaskItem();
        var cancellationToken = CancellationToken.None;

        _mockValidator.Setup(v => v.ValidateAsync(request, cancellationToken))
            .ReturnsAsync(new ValidationResult());

        _mockMapper.Setup(m => m.Map<TaskItem>(request)).Returns(taskItem);
        _mockRepository.Setup(r => r.CreateTaskItem(taskItem, cancellationToken)).ReturnsAsync(false);

        var result = await _useCase.Handle(request, cancellationToken);

        result.Response.Should().BeFalse();
        result.Message.Should().Contain("Error");
    }
}
