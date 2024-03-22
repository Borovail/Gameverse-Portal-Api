namespace Back_End.Extensions
{
    public static class PoliciesServiceExtensions
    {
        public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("UserAccess", policy => policy.RequireRole("User", "Developer", "Admin"));
                options.AddPolicy("DeveloperAccess", policy => policy.RequireRole("Developer", "Admin"));
                options.AddPolicy("AdminAccess", policy => policy.RequireRole("Admin"));
            });

            return services;
        }

        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("https://localhost:5173") // Замените этим адресом ваш фронтенд
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            return services;

        }
    }
}
