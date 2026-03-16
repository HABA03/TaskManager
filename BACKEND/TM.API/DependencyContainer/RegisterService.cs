using FluentValidation;
using TM.Application.DTO.TaskItem.Create;
using TM.Application.DTO.TaskItem.GetById;
using TM.Application.DTO.TaskItem.Remove;
using TM.Application.DTO.TaskItem.Update;
using TM.Domain.Interface;
using TM.Infrastructure.Repositories.TaskItemRepository;

namespace TM.API.DependencyContainer.RegisterService;

public class RegisterService
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<ITaskItemRepository, TaskItemRepository>();
        
        services.AddScoped<IValidator<CreateTaskRequest>, TM.Application.FluentValidation.TaskItemValidation.CreateTaskRequestValidation>(); 
        services.AddScoped<IValidator<UpdateTaskRequest>, TM.Application.FluentValidation.TaskItemValidation.UpdateTaskRequestValidation>();
        services.AddScoped<IValidator<GetTaskByIdRequest>, TM.Application.FluentValidation.TaskItemValidation.GetTaskByIdRequestValidation>();
        services.AddScoped<IValidator<RemoveTaskRequest>, TM.Application.FluentValidation.TaskItemValidation.RemoveTaskRequestValidation>();
    }
}