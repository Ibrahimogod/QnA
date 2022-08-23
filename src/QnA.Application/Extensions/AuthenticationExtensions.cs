namespace Microsoft.AspNetCore.Builder;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddUserAuthentication(this IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtOptions:SigningKey"])),
                    NameClaimType = JwtClaimTypes.Name,
                    ValidIssuer = configuration["JwtOptions:Issuer"],
                    ValidAudience = configuration["JwtOptions:Audience"],
                };
            });

        return services;
    }
}

