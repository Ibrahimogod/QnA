namespace Microsoft.AspNetCore.Builder;

public static class RepositoriesExtensions
{
    public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepository<Question>, QuestionRepository>();
        services.AddScoped<IRepository<Answer>, AnswerRepository>();
        services.AddScoped<IRepository<Vote>, VoteRepository>();

        return services;
    }
}
