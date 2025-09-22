using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ShopDesk.Domain.Entities;

namespace ShopDesk.Persistance.SeedData
{
    public static class ApplicationDbContextSeed
    {
        /// <summary>
        /// Seeds the database with a default Admin role and an Admin user.
        /// </summary>
        /// <param name="userManager">The ASP.NET Core Identity UserManager.</param>
        /// <param name="roleManager">The ASP.NET Core Identity RoleManager.</param>
        /// <param name="configuration">The application configuration to get default password.</param>
        public static async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            string adminRoleName = "Admin";
            if (!await roleManager.RoleExistsAsync(adminRoleName))
            {
                await roleManager.CreateAsync(new IdentityRole(adminRoleName));
            }

            string adminEmail = "admin@shopdesk.com";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                // Create a new user object
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Admin",
                    EmailConfirmed = true
                };

                string adminPassword = configuration["AdminUser:DefaultPassword"] ?? "Admin@1234";
                
                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, adminRoleName);
                }
            }
        }
    }
}
