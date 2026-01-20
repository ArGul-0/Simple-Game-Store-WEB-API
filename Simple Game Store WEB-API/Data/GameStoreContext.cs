using Microsoft.EntityFrameworkCore;
using Simple_Game_Store_WEB_API.DTOs;
using Simple_Game_Store_WEB_API.Entities;

namespace Simple_Game_Store_WEB_API.Data
{
    public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options) // DbContext for Game Store
    {
        public DbSet<Game> Games => Set<Game>(); // DbSet for Games entity
        public DbSet<Genre> Genres => Set<Genre>(); // DbSet for Genres entity



        protected override void OnModelCreating(ModelBuilder modelBuilder) // Configure model creation
        {
            modelBuilder.Entity<Genre>().HasData( // Seed initial data for Genres
            new Genre { ID = 1, Name = "Action-Adventure" },
            new Genre { ID = 2, Name = "Management" },
            new Genre { ID = 3, Name = "RPG" },
            new Genre { ID = 4, Name = "Simulation" },
            new Genre { ID = 5, Name = "Strategy" },
            new Genre { ID = 6, Name = "Sports" },
            new Genre { ID = 7, Name = "Puzzle" },
            new Genre { ID = 8, Name = "Horror" },
            new Genre { ID = 9, Name = "Action" },
            new Genre { ID = 10, Name = "Adventure" }
            );

            modelBuilder.Entity<Game>().HasData( // Seed initial data for Games
                new Game
                {
                    ID = 1,
                    Name = "Gta 5",
                    GenreID = 1, // Action-Adventure
                    Price = 59.99m,
                    ReleaseDate = new DateOnly(2013, 9, 17)
                },
                new Game
                {
                    ID = 2,
                    Name = "Watch Dogs 1",
                    GenreID = 1, // Action-Adventure
                    Price = 39.99m,
                    ReleaseDate = new DateOnly(2014, 5, 26)
                },
                new Game
                {
                    ID = 3,
                    Name = "Cities: Skylines",
                    GenreID = 2, // Management
                    Price = 29.99m,
                    ReleaseDate = new DateOnly(2015, 3, 10)
                },
                new Game
                {
                    ID = 4,
                    Name = "Fran Bow",
                    GenreID = 8, // Horror
                    Price = 4.99m,
                    ReleaseDate = new DateOnly(2015, 8, 27)
                },
                new Game
                {
                    ID = 5,
                    Name = "Cry of Fear",
                    GenreID = 8, // Horror
                    Price = 0m,
                    ReleaseDate = new DateOnly(2013, 4, 25)
                },
                new Game
                {
                    ID = 6,
                    Name = "Sally Face",
                    GenreID = 10, // Adventure
                    Price = 14.99m,
                    ReleaseDate = new DateOnly(2016, 8, 16)
                },
                new Game
                {
                    ID = 7,
                    Name = "Silent Hill 2 Remake",
                    GenreID = 8, // Horror
                    Price = 29.99m,
                    ReleaseDate = new DateOnly(2024, 10, 8)
                }
            );
        }
    }
}
