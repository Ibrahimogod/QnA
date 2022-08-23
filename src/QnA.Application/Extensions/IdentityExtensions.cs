namespace Microsoft.AspNetCore.Builder;

public static class IdentityExtensions
{
    public static IServiceCollection AddApplicationIdentity(this IServiceCollection services)
    {
        services.AddIdentityCore<User>(options =>
        {
            options.ClaimsIdentity = new ClaimsIdentityOptions()
            {
                EmailClaimType = JwtClaimTypes.Email,
                UserIdClaimType = JwtClaimTypes.Id,
                UserNameClaimType = JwtClaimTypes.PreferredUserName,
            };
            options.Password = new PasswordOptions()
            {
                RequireDigit = false,
                RequireNonAlphanumeric = false,
                RequireLowercase = false,
                RequireUppercase = false
            };
        })
          .AddRoles<UserRole>()
          .AddEntityFrameworkStores<QnADbContext>()
          .AddSignInManager();

        return services;
    }
}