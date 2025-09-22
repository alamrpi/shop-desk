
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopDesk.Domain.Entities;
using ShopDesk.Persistance;
using ShopDesk.Persistance.SeedData;

namespace ShopDesk.UI.Extensions
{
    public static class DatabaseExtensions
    {
        /// <summary>
        /// Applies any pending EF Core migrations and seeds the database with default data.
        /// This is an extension method for IHost.
        /// </summary>
        /// <param name="app">The IHost instance to extend.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public static async Task ApplyMigrationsAndSeedDataAsync(this IHost app)
        {
            // A scope is created to resolve services
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>(); // Logger for Program class

                try
                {
                    logger.LogInformation("Starting database initialization...");

                    var dbContext = services.GetRequiredService<ApplicationDbContext>();
                    await dbContext.Database.MigrateAsync();
                    logger.LogInformation("Database migrations applied successfully.");

                    // --- Seed Data ---
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var configuration = services.GetRequiredService<IConfiguration>();

                    await ApplicationDbContextSeed.SeedAdminUserAsync(userManager, roleManager, configuration);
                    logger.LogInformation("Admin user seeding completed successfully.");

                    logger.LogInformation("Database initialization finished.");
                }
                catch (Exception ex)
                {
                    // Log any errors that occur during the process
                    logger.LogError(ex, "An error occurred during database initialization.");
                }
            }
        }
    }
}