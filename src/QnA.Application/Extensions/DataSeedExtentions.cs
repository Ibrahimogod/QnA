namespace Microsoft.AspNetCore.Builder;

public static class DataSeedExtentions
{
    public static IApplicationBuilder UseDataSeeding(this IApplicationBuilder app)
    {
        return app;
    }
}