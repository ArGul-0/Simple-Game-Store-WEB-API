using Simple_Game_Store_WEB_API.DTOs;
using Microsoft.OpenApi;

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



            List<GameDTO> games =
            [
                new GameDTO(1, "Gta 5", "Action-Adventure", 59.99m, new DateOnly(2013, 9, 17)),
                new GameDTO(2, "Watch Dogs 1", "Action-Adventure", 39.99m, new DateOnly(2014, 5, 26)),
                new GameDTO(3, "Cities: Skylines", "Management", 29.99m, new DateOnly(2015, 3, 10)),
                new GameDTO(4, "Fran Bow", "Horror", 4.99m, new DateOnly(2015, 8, 27)),
                new GameDTO(5, "Cry of Fear", "Horror", 0m, new DateOnly(2013, 4, 25)),
                new GameDTO(6, "Sally Face", "Adventure", 14.99m, new DateOnly(2016, 8, 16)),
                new GameDTO(7, "Silent Hill 2 Remake", "Horror-Adventure" ,29.99m, new DateOnly(2024, 10, 8))
            ];

            const string GetGameEndpointName = "GetGame";

            // GET All Games
            app.MapGet("/Games", () => games);

            // GET Game
            app.MapGet("/Games/{ID}", (int ID) =>
            {
                var game = games.FirstOrDefault(g => g.ID == ID);
                return game is not null ? Results.Ok(game) : Results.NotFound();
            }).WithName(GetGameEndpointName);

            // POST Game
            app.MapPost("/Games", (CreateGameDTO newGame) =>
            {
                GameDTO game = new(
                    games.Count + 1,
                    newGame.Name,
                    newGame.Genre,
                    newGame.Price,
                    newGame.ReleaseDate
                    );

                games.Add(game);

                return Results.CreatedAtRoute(GetGameEndpointName, new {ID = game.ID}, game);
            });

            // PUT Game
            app.MapPut("/Games/{ID}", (int ID, UpdateGameDTO updatedGame) =>
            {
                var gameIndex = games.FindIndex(g => g.ID == ID);
                if (gameIndex == -1)
                {
                    return Results.NotFound();
                }

                GameDTO game = new(
                    ID,
                    updatedGame.Name,
                    updatedGame.Genre,
                    updatedGame.Price,
                    updatedGame.ReleaseDate
                    );
                games[gameIndex] = game;

                return Results.NoContent();
            });

            // DELETE Game
            app.MapDelete("/Games/{ID}", (int ID) =>
            {
                var gameIndex = games.FindIndex(g => g.ID == ID);
                if (gameIndex == -1)
                {
                    return Results.NotFound();
                }

                games.RemoveAt(gameIndex);

                return Results.NoContent();
            });

            app.Run();
        }
    }
}