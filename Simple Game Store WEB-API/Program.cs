
using Simple_Game_Store_WEB_API.DTOs;

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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            List<GameDTO> games = 
            [
                new GameDTO(1, "Civilization VI", "4X", 59.99m, new DateOnly(2016, 10, 21)),
                new GameDTO(2, "Stellaris", "4X", 39.99m, new DateOnly(2016, 5, 9)),
                new GameDTO(3, "Cities: Skylines", "Management", 29.99m, new DateOnly(2015, 3, 10)),
                new GameDTO(4, "Planet Zoo", "Management", 44.99m, new DateOnly(2019, 11, 5)),
                new GameDTO(5, "Endless Legend", "4X", 29.99m, new DateOnly(2014, 9, 18))
            ];



            app.Run();
        }
    }
}
