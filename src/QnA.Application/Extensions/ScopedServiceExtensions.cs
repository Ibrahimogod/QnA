namespace Microsoft.AspNetCore.Builder;

public static class ScopedServiceExtensions
{
    public static IServiceCollection AddRequiredService(this IServiceCollection services)
    {

        services.AddScoped<IQuestionService, QuestionService>();

        return services;
    }
}

