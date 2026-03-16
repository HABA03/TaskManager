using FluentValidation;
using TM.Application.DTO.TaskItem.Remove;

namespace TM.Application.FluentValidation.TaskItemValidation;

public class RemoveTaskRequestValidation : AbstractValidator<RemoveTaskRequest>
{
    public RemoveTaskRequestValidation()
    {
        RuleFor(t => t.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0")
            .NotNull().WithMessage("Id cant be null");
    }
}