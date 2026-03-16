using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TM.API.AutoMapperProfile.MapperProfile;
using TM.API.DependencyContainer.RegisterService;
using TM.Application.UseCase.TaskItemUseCases;
using TM.Infrastructure.Context.TMContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TMContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(options =>
{
    options.AddProfile(typeof(MapperProfile));
});

builder.Services.AddControllers();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = null;
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

RegisterService.RegisterServices(builder.Services);

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(typeof(CreateTaskItemUseCase).Assembly);
});

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

app.UseHttpsRedirection();


app.Run();