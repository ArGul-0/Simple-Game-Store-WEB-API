using Microsoft.EntityFrameworkCore;

namespace Simple_Game_Store_WEB_API.Data
{
    public static class DataExtensions
    {
        /// <summary>
        /// Applies any pending migrations for the context to the database. Will create the database if it does not already exist.
        /// </summary>
        /// <remarks>
        /// This method is typically called at application startup to ensure the database is up to date.
        /// </remarks>
        public static void MigrateDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
            dbContext.Database.Migrate();
        }
    }
}
