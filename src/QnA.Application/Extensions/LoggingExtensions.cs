namespace Microsoft.AspNetCore.Builder;

public static class LoggingExtensions
{
    public static IServiceCollection AddApplocationLogger(this IServiceCollection services)
    {
        return services.AddLogging();

    }
}
