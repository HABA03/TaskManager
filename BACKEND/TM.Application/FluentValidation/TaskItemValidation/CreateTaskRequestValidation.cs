using FluentValidation;
using TM.Application.DTO.TaskItem.Create;

namespace TM.Application.FluentValidation.TaskItemValidation;

public class CreateTaskRequestValidation : AbstractValidator<CreateTaskRequest>
{
    public CreateTaskRequestValidation()
    {
        RuleFor(t => t.Name)
            .NotNull().WithMessage("Name cant be null")
            .NotEmpty().WithMessage("Name cant be empty")
            .Length(3, 10).WithMessage("3 characters at least, 10 like max");

        RuleFor(t => t.Description)
            .NotNull().WithMessage("Description cant be null")
            .NotEmpty().WithMessage("Description cant be empty")
            .Length(10, 60).WithMessage("10 characters at least, 60 like max");
    }
}