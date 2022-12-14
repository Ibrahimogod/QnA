namespace Microsoft.AspNetCore.Builder;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration configuration)
    {
        var dbConnnection = configuration.GetConnectionString("QnAConnection");

        // Add services to the container.
        services.AddApplocationLogger();
        services.AddMemoryCache();
        services.AddMediatR(typeof(ApplicationServiceExtensions).Assembly);

        services.AddApplicationIdentity();
        services.AddApplicationData(dbConnnection);
        services.AddUserAuthentication(configuration);
        services.AddApplicationRepositories();
        services.AddRequiredService();

        //Health Check
        services.AddApplicationHealthChecks(dbConnnection);

        return services;
    }
}

