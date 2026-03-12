using MediatR;

namespace TM.Application.DTO.TaskItem.Remove;

public class RemoveTaskRequest : IRequest<RemoveTaskResponse>
{
    public int Id {get; set;}
}