using MediatR;

namespace TM.Application.DTO.TaskItem.Create;

public class CreateTaskRequest : IRequest<CreateTaskResponse>
{
    public string Name {get; set;} = string.Empty;
    public string Description {get; set;} = string.Empty;
}