using System.Reflection;
using Backend.Core.Entities;
using Backend.Core.Interfaces.Repositories;
using Backend.Core.Interfaces.Services;
using Backend.Core.Services;
using Backend.Database;
using Backend.Infrastructure.Database;
using Backend.Infrastructure.Repositories;

namespace Backend.API.Dependencies;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IUsersService, UsersServices>();
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, UsersRepository>();
        return services;
    }

    public static IServiceCollection AddGlobalErrorHandling(this IServiceCollection services)
    {
        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
            };
        });
        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoSettings = configuration.GetSection(DbConstants.MongoDbSettings).Get<MongoDbSettings>();

        Console.WriteLine($"MongoDbSettings: {mongoSettings!.ConnectionString}, {mongoSettings.DatabaseName}");

        services.AddSingleton(serviceProvider =>
        {
            return new MongoDbContext(mongoSettings!.ConnectionString, mongoSettings!.DatabaseName);
        });

        services.AddSingleton(serviceProvider =>
        {
            var context = serviceProvider.GetRequiredService<MongoDbContext>();
            return context.Users;
        });

        return services;
    }

    public static IServiceCollection AddFrontendCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend",
                policy => policy
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
            );
        });
        return services;
    }
}