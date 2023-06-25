using LibraryPersistenceLayer.Configurations;
using Microsoft.EntityFrameworkCore;

namespace libraryWebApi
{
    public static class Bootstrapper
    {
        public static async Task MigrateAsync(this WebApplication app)
        {
            var provider = app.Services.CreateScope();
            var context = provider.ServiceProvider.GetRequiredService<LibraryDbContext>();
            await context.Database.MigrateAsync();
        }
    }
}
