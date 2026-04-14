using Microsoft.AspNetCore.Authentication.JwtBearer;
using Simple_Game_Store_WEB_API.Services.Genres;
using Simple_Game_Store_WEB_API.Services.Games;
using Simple_Game_Store_WEB_API.Services.Auth;
using Simple_Game_Store_WEB_API.Validators;
using Simple_Game_Store_WEB_API.Endpoints;
using Simple_Game_Store_WEB_API.Mappers;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.IdentityModel.Tokens;
using Simple_Game_Store_WEB_API.DTOs;
using Simple_Game_Store_WEB_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using FluentValidation;
using System.Text;

namespace Simple_Game_Store_WEB_API
{
    public class Program
    {
        public static async Task Main(string[] args) // Main Method - Application Entry Point
        {
            var builder = WebApplication.CreateBuilder(args); // Create A WebApplication Builder



            builder.Services.AddOpenApi(); // Learn More About Configuring OpenAPI At https://aka.ms/aspnet/openapi

            builder.Services.AddEndpointsApiExplorer(); // Add Endpoints API Explorer Services

            builder.Services.AddValidation(); // Add validation services
            builder.Services.AddProblemDetails(); // Add Problem Details services

            var connString = builder.Configuration.GetConnectionString("DefaultConnection"); // Get Connection String From Configuration

            builder.Services.AddDbContext<GameStoreContext>(options => // Use PostgreSQL Database
                options.UseNpgsql(connString)); // PostgreSQL provider

            builder.Services.AddScoped<IValidator<CreateGameDTO>, CreateGameDTOValidator>(); // Register CreateGameDTO Validator, NuGet FluentValidation Package
            builder.Services.AddScoped<IValidator<UpdateGameDTO>, UpdateGameDTOValidator>(); // Register UpdateGameDTO Validator, NuGet FluentValidation Package

            builder.Services.AddScoped<IGameMapper, GameMapper>(); // Register GameMapper Service, Scoped Lifetime
            builder.Services.AddScoped<IGenreMapper, GenreMapper>(); // Register GenreMapper Service, Scoped Lifetime

            builder.Services.AddScoped<IAuthService, AuthService>(); // Register AuthService, Scoped Lifetime
            builder.Services.AddScoped<ITokenService, TokenService>(); // Register TokenService, Scoped Lifetime

            builder.Services.AddScoped<IValueHasher, Argon2Hasher>(); // Register Argon2Hasher, Scoped Lifetime

            builder.Services.AddScoped<IGamesService, GamesService>(); // Register GamesService, Scoped Lifetime
            builder.Services.AddScoped<IGenreService, GenreService>(); // Register GenreService, Scoped Lifetime


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // Add Authentication Services With JWT Bearer Scheme
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters // Configure Token Validation Parameters
                {
                    ValidateIssuer = true, // Enable Issuer Validation To Ensure Token Is Issued By A Trusted Authority
                    ValidIssuer = builder.Configuration["JwtOptions:Issuer"], // Set The Valid Issuer To The Value From Configuration
                    ValidateAudience = true, // Enable Audience Validation To Ensure Token Is Intended For This API
                    ValidAudience = builder.Configuration["JwtOptions:Audience"], // Set The Valid Audience To The Value From Configuration
                    ValidateLifetime = true, // Enable Lifetime Validation To Ensure Tokens Expire
                    ValidateIssuerSigningKey = true, // Enable Issuer Signing Key Validation To Ensure Token Integrity
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        builder.Configuration["JwtOptions:SecretKey"]!)), // Use A Symmetric Security Key Derived From The Secret Key In Configuration
                };

                options.Events = new JwtBearerEvents // Configure JWT Bearer Events For Better Error Handling
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies[builder.Configuration["JwtOptions:NameInCookies"]!];


                        return Task.CompletedTask;
                    }
                };
            });

            builder.Services.AddAuthorization(); // Add Services To The Container.

            builder.Services.AddSwaggerGen(options => // Configure Swagger
            {
                options.SwaggerDoc("v1", new OpenApiInfo // Define Swagger Document
                {
                    Version = "v1",
                    Title = "Simple Game Store WEB-API",
                    Description = "A Simple Game Store WEB-API Created On ASP.NET Core"
                });
            });

            var app = builder.Build(); // Build the application



            app.MapOpenApi();

            app.UseSwagger();
            app.UseSwaggerUI(options => // Configure Swagger UI
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty; // Set Swagger UI At The App's Root
            });

            app.UseHttpsRedirection(); // Enable HTTPS Redirection

            app.UseAuthentication(); // Enable Authentication Middleware
            app.UseAuthorization(); // Enable Authorization Middleware

            app.UseCookiePolicy(
                new CookiePolicyOptions
                {
                    HttpOnly = HttpOnlyPolicy.Always, // Set Cookies To HttpOnly For Security
                    Secure = CookieSecurePolicy.Always, // Set Cookies To Secure For Security
                    MinimumSameSitePolicy = SameSiteMode.Strict // Set SameSite Policy To Strict For Security
                }
            );



            app.MapGamesEndpoints(); // Map Games Endpoints
            app.MapGenresEndpoints(); // Map Genres Endpoints
            app.MapAuthEndpoints(); // Map Auth Endpoints

            app.MapGet("/health", () => Results.Ok("Healthy!")); // Health Check Endpoint

            await app.MigrateDatabaseAsync(); // Apply Database Migrations



            app.Run(); // Run The Application
        }
    }
}