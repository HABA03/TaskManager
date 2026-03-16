
using FluentValidation;
using MediatR;
using TM.Application.DTO.TaskItem.Remove;
using TM.Domain.Interface;

namespace TM.Application.UseCase.TaskItemUseCases;

public class RemoveTaskItemUseCase : IRequestHandler<RemoveTaskRequest, RemoveTaskResponse>
{
    private readonly IValidator<RemoveTaskRequest> _validator;
    private readonly ITaskItemRepository _repository;

    public RemoveTaskItemUseCase(IValidator<RemoveTaskRequest> validator, ITaskItemRepository repository)
    {
        _validator = validator;
        _repository = repository;
    }

    public async Task<RemoveTaskResponse> Handle(RemoveTaskRequest request, CancellationToken cancellationToken)
    {
        var validationResponse = await _validator.ValidateAsync(request, cancellationToken);

        if(!validationResponse.IsValid)
            return new RemoveTaskResponse{ Response = false, Message = string.Join(",", validationResponse.Errors.Select(e => e.ErrorMessage))};

        var response = await _repository.RemoveTaskItem(request.Id, cancellationToken);

        return new RemoveTaskResponse{ Response = response, Message = response ? "Removed" : "Error removing" };
    }
}