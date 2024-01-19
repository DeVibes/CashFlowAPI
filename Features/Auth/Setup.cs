using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace CashFlowAPI.Features.Auth;

public static class Setup
{
    private const string CLAIM_TYPE = "permissions";
    private static List<string> Permissions = new List<string> {"read:approved", "user"};
    public static IServiceCollection AddAuthServices(this IServiceCollection services, string domain, string audience)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => 
            {
                options.Authority = domain;
                options.Audience = audience;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier,
                    ValidAudience = audience,
                    ValidIssuer = domain
                };
            });
            services.AddAuthorization(options => 
            {
                foreach (var permission in Permissions)
                {
                    options.AddPolicy(permission, policy => policy
                        .RequireAuthenticatedUser()
                        .RequireClaim(CLAIM_TYPE, permission));
                }
            });
        return services;
    }
}