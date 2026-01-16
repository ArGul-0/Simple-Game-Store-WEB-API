using Microsoft.EntityFrameworkCore;

namespace Simple_Game_Store_WEB_API.Data
{
    public static class DataExtensions
    {
        public static void MigrateDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
            dbContext.Database.Migrate();
        }
    }
}
