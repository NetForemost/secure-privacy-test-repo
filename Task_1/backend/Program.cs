// DI CONFIGURATION
using System.Globalization;
using Backend.API.Dependencies;
using Backend.API.RequestPipeline;

var builder = WebApplication.CreateBuilder(args);
{
    var defaultCulture = new CultureInfo("en-US");
    CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
    CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;

    builder.Services
        .AddGlobalErrorHandling()
        .AddServices()
        .AddRepositories()
        .AddPersistence(builder.Configuration)
        .AddFrontendCors()
        .AddControllers();
}

// REQUEST PIPELINE CONFIGURATION
var app = builder.Build();
{
    ILogger<WebApplication>? logger = app.Services.GetRequiredService<ILogger<WebApplication>>();

    app.MapControllers();
    app.UseGlobalErrorHandling(logger);
    app.UseCors("AllowFrontend");
}
app.Run();
