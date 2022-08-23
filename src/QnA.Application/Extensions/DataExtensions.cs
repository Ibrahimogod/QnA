namespace Microsoft.AspNetCore.Builder;

public static class DataExtensions
{
    public static IServiceCollection AddApplicationData(this IServiceCollection services, string dbConnnection)
    {
       return services.AddDbContext<QnADbContext>(options =>
       {
            options.UseSqlServer(dbConnnection);
       },ServiceLifetime.Singleton, ServiceLifetime.Singleton)
        .AddDbContextFactory<QnADbContext>(options =>
        {
            options.UseSqlServer(dbConnnection);
        }, ServiceLifetime.Singleton);
    }
}
