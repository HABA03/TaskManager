using MediatR;
using Microsoft.AspNetCore.Mvc;
using TM.Application.DTO.TaskItem.Create;
using TM.Application.DTO.TaskItem.GetAll;
using TM.Application.DTO.TaskItem.GetById;
using TM.Application.DTO.TaskItem.Remove;
using TM.Application.DTO.TaskItem.Update;

namespace TM.API.Controllers.TaskItemController;

[ApiController]
[Route("TaskItem")]
public class TaskItemController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskItemController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Create")]
    [ProducesResponseType(typeof(CreateTaskResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateTask(CreateTaskRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPut("Update")]
    [ProducesResponseType(typeof(UpdateTaskResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateTask(UpdateTaskRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPut("Remove")]
    [ProducesResponseType(typeof(RemoveTaskResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> RemoveTask(RemoveTaskRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPut("GetById")]
    [ProducesResponseType(typeof(GetTaskByIdResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTaskById(GetTaskByIdRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpGet("GetAll")]
    [ProducesResponseType(typeof(List<GetAllTaskResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllTask(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllTaskRequest(), cancellationToken);
        return Ok(response);
    }
}