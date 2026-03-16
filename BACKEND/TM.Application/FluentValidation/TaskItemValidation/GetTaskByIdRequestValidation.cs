using FluentValidation;
using TM.Application.DTO.TaskItem.GetById;

namespace TM.Application.FluentValidation.TaskItemValidation;

public class GetTaskByIdRequestValidation : AbstractValidator<GetTaskByIdRequest>
{
    public GetTaskByIdRequestValidation()
    {
        RuleFor(t => t.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0")
            .NotNull().WithMessage("Id cant be null");
    }
}