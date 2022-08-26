namespace Microsoft.AspNetCore.Builder;

public static class ExceptionHandlerExtensions
{

    public static IApplicationBuilder UseApplicationExceptionHandler(this IApplicationBuilder app)
    {

        app.Use(async (context, next) =>
        {
            try
            {
                await next();
            }
            catch (Exception ex)
            {
                using var scopedService = app.ApplicationServices.CreateScope();
                var logger = scopedService.ServiceProvider.GetRequiredService<ILogger<Exception>>();
                logger.LogError(ex, ex.Message);

                context.Response.Clear();
                context.Response.StatusCode = 500;
            }
        });
        return app;
    }
}
