using MediatR;
using TM.Application.DTO.TaskItem.GetById;

namespace TM.Application.DTO.TaskItem.GetAll;

public class GetAllTaskRequest : IRequest<List<GetAllTaskResponse>>
{
}