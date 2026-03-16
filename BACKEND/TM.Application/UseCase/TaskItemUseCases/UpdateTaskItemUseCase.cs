using AutoMapper;
using FluentValidation;
using MediatR;
using TM.Application.DTO.TaskItem.Update;
using TM.Domain.Entity;
using TM.Domain.Interface;

namespace TM.Application.UseCase.TaskItemUseCases.UpdateTaskItemUseCase;

public class UpdateTaskItemUseCase : IRequestHandler<UpdateTaskRequest, UpdateTaskResponse>
{
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateTaskRequest> _validator;
    private readonly ITaskItemRepository _repository;

    public UpdateTaskItemUseCase(IMapper mapper, IValidator<UpdateTaskRequest> validator, ITaskItemRepository repository)
    {
        _mapper = mapper;
        _validator = validator;
        _repository = repository;
    }

    public async Task<UpdateTaskResponse> Handle(UpdateTaskRequest request, CancellationToken cancellationToken)
    {
        var validationResponse = await _validator.ValidateAsync(request, cancellationToken);

        if(!validationResponse.IsValid)
            return new UpdateTaskResponse{ Response = false, Message = string.Join(",", validationResponse.Errors.Select(e => e.ErrorMessage))};

        var response = await _repository.UpdateTaskItem(_mapper.Map<TaskItem>(request), cancellationToken);

        return new UpdateTaskResponse{ Response = response, Message = response ? "Updated" : "Error updating" };
    }
}