using Back_End.Middleware;
using Back_End.Utils;
using Back_End.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Registering custom JWT token service
builder.Services.AddScoped<JwtTokenService>();

// Adding custom service extensions to the container
builder.Services
    .AddJwtAuthentication(builder.Configuration) // Adding JWT authentication
    .AddAuthorizationPolicies() // Adding custom authorization policies
    .AddCorsPolicy() // Configuring CORS policy
    .AddDbConfiguration(builder.Configuration) // Configuring DbContext
    .AddIdentityConfiguration(); // Configuring Identity

// Adding controllers to the service container
builder.Services.AddControllers();

// Setting up Swagger for API documentation
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer(); // Required for Swagger

var app = builder.Build();

// Configuring the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    // Enabling Swagger in development environment
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Exception handling middleware for global error handling
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Applying CORS policy
app.UseCors("AllowSpecificOrigin");

// Enforcing HTTPS redirection
app.UseHttpsRedirection();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// Routing controllers
app.MapControllers();

// Starting the application
app.Run();
