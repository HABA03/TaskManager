using AutoMapper;
using MediatR;
using TM.Application.DTO.TaskItem.GetAll;
using TM.Domain.Interface;

namespace TM.Application.UseCase.TaskItemUseCases;

public class GetAllTaskItemUseCase : IRequestHandler<GetAllTaskRequest, List<GetAllTaskResponse>>
{
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly IMapper _mapper;

    public GetAllTaskItemUseCase(ITaskItemRepository taskItemRepository, IMapper mapper)
    {
        _taskItemRepository = taskItemRepository;
        _mapper = mapper;
    }

    public async Task<List<GetAllTaskResponse>> Handle(GetAllTaskRequest request, CancellationToken cancellationToken)
    {
        var tasksInformation = await _taskItemRepository.GetAllTaskItem(cancellationToken);
        return _mapper.Map<List<GetAllTaskResponse>>(tasksInformation);
    }
}