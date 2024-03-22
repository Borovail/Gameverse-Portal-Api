using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Back_End.Extensions
{
    public static class AuthenticationServiceExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var authenticationBuilder = services.AddAuthenticationSchemes();

            authenticationBuilder
               .AddJwtBearerConfiguration(configuration);

            return services;
        }

        private static AuthenticationBuilder AddAuthenticationSchemes(this IServiceCollection services)
        {
            return services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });
        }

        private static AuthenticationBuilder AddJwtBearerConfiguration(this AuthenticationBuilder builder, IConfiguration configuration)
        {
            builder.AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                };

                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();
                        await Console.Out.WriteLineAsync("OnChallenge");

                        var failure = context.AuthenticateFailure;

                        if (failure != null)
                        {
                            var serviceProvider = builder.Services.BuildServiceProvider();
                            var logger = serviceProvider.GetRequiredService<ILogger>();
                            logger.LogError($"Authentication failed: {failure.Message}");
                        }

                        var result = new
                        {
                            Success = false,
                            Message = "You are not authorized.",
                            StatusCode = StatusCodes.Status401Unauthorized
                        };
                        var resultJson = JsonSerializer.Serialize(result);
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(resultJson);
                    },
                    OnForbidden = async context =>
                    {
                        await Console.Out.WriteLineAsync("OnForbidden");

                        var result = new
                        {
                            Success = false,
                            Message = "You do not have access to this resource.",
                            StatusCode = StatusCodes.Status403Forbidden
                        };
                        var resultJson = JsonSerializer.Serialize(result);
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(resultJson);
                    }
                };
            });

            return builder;
        }


     

    }
}
