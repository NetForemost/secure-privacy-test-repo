using Backend.Core.Errors;
using Microsoft.AspNetCore.Diagnostics;

namespace Backend.API.RequestPipeline;

public static class WebApplicationExtensions
{
    public static WebApplication UseGlobalErrorHandling(this WebApplication app, ILogger logger)
    {
        app.UseExceptionHandler("/error");
        app.Map("/error", (HttpContext httpContext) => {
            Exception? exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error; 

            if(exception is null)
            {
                // handling unexpected case
                return Results.Problem();
            }

            logger.LogError(exception, "An error occurred while processing the request.");

            // custom global error handling logic
            return exception switch
            {
                ServiceException serviceException => Results.Problem(
                    statusCode: serviceException.StatusCode, 
                    detail: serviceException.ErrorMessage,
                    title: "An error ocurred",
                    instance: httpContext.TraceIdentifier
                ),
                _ => Results.Problem(
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: "An unexpected error occurred",
                    instance: httpContext.TraceIdentifier
                )
            };
        });

        return app;
    }
}