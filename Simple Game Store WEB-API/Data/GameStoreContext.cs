using Simple_Game_Store_WEB_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Simple_Game_Store_WEB_API.Data
{
    public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
    {
        public DbSet<Game> Games => Set<Game>();
        public DbSet<Genre> Genres => Set<Genre>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData(
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
        }
    }
}
