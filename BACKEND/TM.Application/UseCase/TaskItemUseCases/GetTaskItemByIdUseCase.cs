using AutoMapper;
using FluentValidation;
using MediatR;
using TM.Application.DTO.TaskItem.GetById;
using TM.Domain.Interface;

namespace TM.Application.UseCase.TaskItemUseCases;

public class GetTaskItemByIdUseCase : IRequestHandler<GetTaskByIdRequest, GetTaskByIdResponse>
{
    private readonly IMapper _mapper;
    private readonly IValidator<GetTaskByIdRequest> _validator;
    private readonly ITaskItemRepository _repository;

    public GetTaskItemByIdUseCase(IMapper mapper, IValidator<GetTaskByIdRequest> validator, ITaskItemRepository repository)
    {
        _mapper = mapper;
        _validator = validator;
        _repository = repository;
    }

    public async Task<GetTaskByIdResponse> Handle(GetTaskByIdRequest request, CancellationToken cancellationToken)
    {
        var validationResponse = await _validator.ValidateAsync(request, cancellationToken);

        if(!validationResponse.IsValid)
            throw new Exception(string.Join(",", validationResponse.Errors.Select(e => e.ErrorMessage)));

        return _mapper.Map<GetTaskByIdResponse>(_repository.GetTaskItemById(request.Id, cancellationToken));
    }
}