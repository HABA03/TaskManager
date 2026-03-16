using AutoMapper;
using FluentValidation;
using MediatR;
using TM.Application.DTO.TaskItem.Create;
using TM.Domain.Entity;
using TM.Domain.Interface;

namespace TM.Application.UseCase.TaskItemUseCases;

public class CreateTaskItemUseCase : IRequestHandler<CreateTaskRequest, CreateTaskResponse>
{
    private readonly IMapper _mapper;
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly IValidator<CreateTaskRequest> _validator;

    public CreateTaskItemUseCase(IMapper mapper, ITaskItemRepository taskItemRepository, IValidator<CreateTaskRequest> validator)
    {
        _mapper = mapper;
        _taskItemRepository = taskItemRepository;
        _validator = validator;
    }

    public async Task<CreateTaskResponse> Handle(CreateTaskRequest request, CancellationToken cancellationToken)
    {
        var validationResponse = await _validator.ValidateAsync(request, cancellationToken);

        if(!validationResponse.IsValid)
        return new CreateTaskResponse{ Response = false, Message = string.Join(", ", validationResponse.Errors.Select(e => e.ErrorMessage)) };

        var response = await _taskItemRepository.CreateTaskItem(_mapper.Map<TaskItem>(request), cancellationToken);

        return new CreateTaskResponse{ Response = response, Message = response ? "Task created" : "Error creating" };
    }
}