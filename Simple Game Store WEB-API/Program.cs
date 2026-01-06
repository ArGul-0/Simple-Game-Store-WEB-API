using Simple_Game_Store_WEB_API.DTOs;
using Microsoft.OpenApi;
using Simple_Game_Store_WEB_API.Endpoints;

namespace Simple_Game_Store_WEB_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddValidation(); // Add validation services
            builder.Services.AddProblemDetails(); // Add Problem Details services

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Simple Game Store WEB-API",
                    Description = "A Simple Game Store WEB-API Created On ASP.NET Core"
                });
            });

            var app = builder.Build();



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();



            app.MapGamesEndpoints();

            app.Run();
        }
    }
}