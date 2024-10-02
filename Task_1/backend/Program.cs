// DI CONFIGURATION
using Backend.API.Dependencies;
using Backend.API.RequestPipeline;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddGlobalErrorHandling()
        .AddServices()
        .AddRepositories()
        .AddPersistence(builder.Configuration)
        .AddControllers();
}

// REQUEST PIPELINE CONFIGURATION
var app = builder.Build();
{
    ILogger<WebApplication>? logger = app.Services.GetRequiredService<ILogger<WebApplication>>();

    app.MapControllers();
    app.UseGlobalErrorHandling(logger);
}
app.Run();
