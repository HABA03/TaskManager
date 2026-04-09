using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TM.API.AutoMapperProfile.MapperProfile;
using TM.API.DependencyContainer.RegisterService;
using TM.Application.UseCase.TaskItemUseCases;
using TM.Infrastructure.Context.TMContext;

var builder = WebApplication.CreateBuilder(args);

// Only register DbContext in non-testing environments
if (!builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddDbContext<TMContext>(options =>
    {
        options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null
            );
        });
    });
}

builder.Services.AddAutoMapper(options =>
{
    options.AddProfile(typeof(MapperProfile));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("corspolicy", opt =>
    {
        opt.AllowAnyMethod();
        opt.AllowAnyHeader();
        opt.AllowAnyOrigin();
    });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});

RegisterService.RegisterServices(builder.Services);

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(typeof(CreateTaskItemUseCase).Assembly);
});

builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TMContext>();

    try
    {
        db.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error applying migrations: " + ex.Message);
    }
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("corspolicy");

app.MapControllers();

// app.UseHttpsRedirection();

app.Run();

public partial class Program { }
