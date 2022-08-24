namespace Microsoft.AspNetCore.Builder;

public static class MigrationExtensions
{
    public static async Task<IApplicationBuilder> MigrateAsync(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<QnADbContext>();
            await db.Database.MigrateAsync();
            await db.Database.EnsureCreatedAsync();
        }

        return app;
    }
}
