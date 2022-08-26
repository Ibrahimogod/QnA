namespace Microsoft.AspNetCore.Builder;

public static class ScopedServiceExtensions
{
    public static IServiceCollection AddRequiredService(this IServiceCollection services)
    {

        services.AddSingleton<IQuestionService, QuestionService>();

        return services;
    }
}

