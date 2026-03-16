using FluentValidation;
using TM.Application.DTO.TaskItem.Create;
using TM.Application.DTO.TaskItem.GetById;
using TM.Application.DTO.TaskItem.Remove;
using TM.Application.DTO.TaskItem.Update;
using TM.Application.FluentValidation.TaskItemValidation;
using TM.Domain.Interface;
using TM.Infrastructure.Repositories.TaskItemRepository;

namespace TM.API.DependencyContainer.RegisterService;

public class RegisterService
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<ITaskItemRepository, TaskItemRepository>();
        
        services.AddScoped<IValidator<CreateTaskRequest>, CreateTaskRequestValidation>();
        services.AddScoped<IValidator<UpdateTaskRequest>, UpdateTaskRequestValidation>();
        services.AddScoped<IValidator<GetTaskByIdRequest>, GetTaskByIdRequestValidation>();
        services.AddScoped<IValidator<RemoveTaskRequest>, RemoveTaskRequestValidation>();
    }
}