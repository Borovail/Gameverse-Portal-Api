﻿using Back_End.Middleware;
using Back_End.Utils;
using Back_End.Extensions;
using Back_End.Services.Interfaces;
using Back_End.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);


// Extract to an extension method
// Registering custom JWT token service
builder.Services.AddScoped<JwtToken>();
builder.Services.AddScoped<IBugService,BugService>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IAccessService, AccessService>();
builder.Services.AddScoped<IAccountService, AccountService>();


// Adding custom service extensions to the container
builder.Services
    .AddDbConfiguration(builder.Configuration) // Configuring DbContext
    .AddIdentityConfiguration() // Configuring Identity
    .AddAuthorizationPolicies() // Adding custom authorization policies
    .AddJwtAuthentication(builder.Configuration) // Adding JWT authentication
    .AddCorsPolicy(); // Configuring CORS policy

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