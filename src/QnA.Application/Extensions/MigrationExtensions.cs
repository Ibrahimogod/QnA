namespace Microsoft.AspNetCore.Builder;

public static class MigrationExtensions
{
    public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<QnADbContext>();
            db.Database.Migrate();
            db.Database.EnsureCreated();
        }

        return app;
    }
}
