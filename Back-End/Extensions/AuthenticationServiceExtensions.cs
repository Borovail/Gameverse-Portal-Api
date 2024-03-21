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
                .AddJwtBearerConfiguration(configuration)
                .AddJwtBearerEventsConfiguration();

            return services;
        }

        private static AuthenticationBuilder AddAuthenticationSchemes(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            return services.AddAuthentication();
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
            });

            return builder;
        }


        private static AuthenticationBuilder AddJwtBearerEventsConfiguration(this AuthenticationBuilder builder)
        {
            builder.AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();
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
