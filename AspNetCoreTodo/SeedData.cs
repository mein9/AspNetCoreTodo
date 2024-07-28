using System;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspNetCoreTodo
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services, ILogger logger)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            await EnsureRolesAsync(roleManager, logger);

            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            await EnsureTestAdminAsync(userManager, logger);
        }

        private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager, ILogger logger)
        {
            logger.LogInformation("Ensuring roles are created.");
            var alreadyExists = await roleManager.RoleExistsAsync(Constants.AdministratorRole);
            if (alreadyExists)
            {
                logger.LogInformation("Administrator role already exists.");
                return;
            }

            var result = await roleManager.CreateAsync(new IdentityRole(Constants.AdministratorRole));
            if (result.Succeeded)
            {
                logger.LogInformation("Created the Administrator role.");
            }
            else
            {
                logger.LogError("Failed to create the Administrator role.");
            }
        }

        private static async Task EnsureTestAdminAsync(UserManager<IdentityUser> userManager, ILogger logger)
        {
            logger.LogInformation("Ensuring the test admin user is created.");
            var testAdmin = await userManager.Users
                .Where(x => x.UserName == "admin@todo.local")
                .SingleOrDefaultAsync();

            if (testAdmin != null)
            {
                logger.LogInformation("Test admin user already exists.");
                return;
            }

            testAdmin = new IdentityUser
            {
                UserName = "admin@todo.local",
                Email = "admin@todo.local"
            };

            var result = await userManager.CreateAsync(testAdmin, "NotSecure123!!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(testAdmin, Constants.AdministratorRole);
                logger.LogInformation("Created the test admin user and assigned the Administrator role.");
            }
            else
            {
                logger.LogError("Failed to create the test admin user.");
            }
        }
    }
}