namespace Microsoft.AspNetCore.Builder;

public static class HealthCheckExtensions
{
    public static IServiceCollection AddApplicationHealthChecks(this IServiceCollection services, string connectionString)
    {
        //adding health check services to container
        services.AddHealthChecks()
            .AddSqlServer(connectionString: connectionString,
                name: "SQL Server Health Check",
                failureStatus: HealthStatus.Unhealthy,
                tags: new[] { "db", "database-connection" })
            .AddDbContextCheck<QnADbContext>(
                name:"QnADbContext Health Check",
                failureStatus:HealthStatus.Unhealthy,
                tags: new[] { "db", "orm" });

        //adding healthchecks UI
        services
            .AddHealthChecksUI(opt =>
            {
                opt.SetEvaluationTimeInSeconds(15); //time in seconds between check
                opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks
                opt.SetApiMaxActiveRequests(1); //api requests concurrency
            })
            .AddInMemoryStorage();

        return services;
    }

    public static IApplicationBuilder UseApplicationHealthChecks(this IApplicationBuilder app)
    {

        app.UseEndpoints(endpoints =>
        {
            //adding endpoint of health check for the health check ui in UI format
            endpoints.MapHealthChecks("/health-api", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            //map healthcheck ui endpoing - default is /healthchecks-ui/
            endpoints.MapHealthChecksUI(options =>
            {
                options.UIPath = "/health-monitor";
                options.PageTitle = "Health Monitor";
            });

        });

        return app;
    }
}
