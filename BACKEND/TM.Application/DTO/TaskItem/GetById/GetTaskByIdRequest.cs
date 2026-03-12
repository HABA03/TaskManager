using MediatR;

namespace TM.Application.DTO.TaskItem.GetById;

public class GetTaskByIdRequest : IRequest<GetTaskByIdResponse>
{
    public int Id {get; set;}
}