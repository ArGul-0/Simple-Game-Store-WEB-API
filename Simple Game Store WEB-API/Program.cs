using Simple_Game_Store_WEB_API.Validators;
using Simple_Game_Store_WEB_API.Endpoints;
using Simple_Game_Store_WEB_API.Mappers;
using Simple_Game_Store_WEB_API.DTOs;
using Simple_Game_Store_WEB_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using FluentValidation;

namespace Simple_Game_Store_WEB_API
{
    public class Program
    {
        public static async Task Main(string[] args) // Main Method - Application Entry Point
        {
            var builder = WebApplication.CreateBuilder(args); // Create A WebApplication Builder



            builder.Services.AddAuthorization(); // Add Services To The Container.

            builder.Services.AddOpenApi(); // Learn More About Configuring OpenAPI At https://aka.ms/aspnet/openapi

            builder.Services.AddEndpointsApiExplorer(); // Add Endpoints API Explorer Services

            builder.Services.AddValidation(); // Add validation services
            builder.Services.AddProblemDetails(); // Add Problem Details services
            builder.Services.AddScoped<IValidator<CreateGameDTO>, CreateGameDTOValidator>(); // Register CreateGameDTO Validator, NuGet FluentValidation Package
            builder.Services.AddScoped<IValidator<UpdateGameDTO>, UpdateGameDTOValidator>(); // Register UpdateGameDTO Validator, NuGet FluentValidation Package

            var connString = builder.Configuration.GetConnectionString("DefaultConnection"); // Get Connection String From Configuration

            builder.Services.AddDbContext<GameStoreContext>(options => // Use PostgreSQL Database
                options.UseNpgsql(connString)); // PostgreSQL provider

            builder.Services.AddSwaggerGen(options => // Configure Swagger
            {
                options.SwaggerDoc("v1", new OpenApiInfo // Define Swagger Document
                {
                    Version = "v1",
                    Title = "Simple Game Store WEB-API",
                    Description = "A Simple Game Store WEB-API Created On ASP.NET Core"
                });
            });

            builder.Services.AddScoped<IGameMapper, GameMapper>(); // Register GameMapper Service, Scoped lifetime
            builder.Services.AddScoped<IGenreMapper, GenreMapper>(); // Register GenreMapper Service, Scoped lifetime

            var app = builder.Build(); // Build the application



            if (app.Environment.IsDevelopment()) // Enable Swagger In Development Environment
            {
                app.MapOpenApi();

                app.UseSwagger();
                app.UseSwaggerUI(options => // Configure Swagger UI
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty; // Set Swagger UI At The App's Root
                });
            }

            app.UseHttpsRedirection(); // Enable HTTPS redirection

            app.UseAuthorization(); // Enable Authorization middleware



            app.MapGamesEndpoints(); // Map Games endpoints
            app.MapGenresEndpoints(); // Map Genres endpoints

            app.MapGet("/health", () => Results.Ok("Healthy!")); // Health Check Endpoint

            await app.MigrateDatabaseAsync(); // Apply database migrations



            app.Run(); // Run the application
        }
    }
}