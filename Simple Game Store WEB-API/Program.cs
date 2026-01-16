using Simple_Game_Store_WEB_API.Endpoints;
using Simple_Game_Store_WEB_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

namespace Simple_Game_Store_WEB_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


 
            builder.Services.AddAuthorization(); // Add services to the container.

            builder.Services.AddOpenApi(); // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            builder.Services.AddEndpointsApiExplorer(); // Add Endpoints API Explorer services

            builder.Services.AddValidation(); // Add validation services
            builder.Services.AddProblemDetails(); // Add Problem Details services

            var connString = builder.Configuration.GetConnectionString("DefaultConnection"); // Get connection string from configuration

            builder.Services.AddDbContext<GameStoreContext>(options => // Use PostgreSQL database
                options.UseNpgsql(connString));

            builder.Services.AddSwaggerGen(options => // Configure Swagger
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Simple Game Store WEB-API",
                    Description = "A Simple Game Store WEB-API Created On ASP.NET Core"
                });
            });



            var app = builder.Build(); // Build the application



            if (app.Environment.IsDevelopment()) // Enable Swagger in Development environment
            {
                app.MapOpenApi();

                app.UseSwagger();
                app.UseSwaggerUI(options => // Configure Swagger UI
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
                });
            }

            app.UseHttpsRedirection(); // Enable HTTPS redirection

            app.UseAuthorization(); // Enable Authorization middleware



            app.MapGamesEndpoints(); // Map Games endpoints



            app.Run(); // Run the application
        }
    }
}