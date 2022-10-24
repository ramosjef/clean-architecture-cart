using Carts.Application.ExternalServices.Users;
using Carts.Infrastructure.ExternalServices.Authentication;

using Microsoft.AspNetCore.Authentication;

namespace Carts.Api.Common.Authentication;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddScoped<IUserService, UserServiceMock>()
            .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = "Test";
                x.DefaultChallengeScheme = "Test";
            })
            .AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(
                "Test",
                options =>
                {
                });

        return services;
    }
}
