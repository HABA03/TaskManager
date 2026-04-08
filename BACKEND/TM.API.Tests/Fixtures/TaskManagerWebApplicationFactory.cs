using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TM.Infrastructure.Context.TMContext;

namespace TM.API.Tests.Fixtures;

public class TaskManagerWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _dbName;

    public TaskManagerWebApplicationFactory()
    {
        _dbName = Guid.NewGuid().ToString();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        
        builder.ConfigureServices(services =>
        {
            // Remove all DbContext related services
            services.RemoveAll(typeof(DbContextOptions));
            services.RemoveAll(typeof(DbContextOptions<TMContext>));
            services.RemoveAll(typeof(TMContext));

            // Add in-memory database for testing (same database for all requests in a test)
            services.AddDbContext<TMContext>(options =>
            {
                options.UseInMemoryDatabase(_dbName);
            }, contextLifetime: ServiceLifetime.Scoped);
        });

        base.ConfigureWebHost(builder);
    }
}

// Extension method helper
internal static class ServiceCollectionExtensions
{
    public static IServiceCollection RemoveAll(this IServiceCollection services, Type serviceType)
    {
        var descriptors = services.Where(d => d.ServiceType == serviceType).ToList();
        foreach (var descriptor in descriptors)
        {
            services.Remove(descriptor);
        }
        return services;
    }
}

