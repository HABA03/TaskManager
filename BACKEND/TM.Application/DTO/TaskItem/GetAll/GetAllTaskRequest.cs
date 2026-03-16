using MediatR;

namespace TM.Application.DTO.TaskItem.GetAll;

public class GetAllTaskRequest : IRequest<List<GetAllTaskResponse>>
{
}