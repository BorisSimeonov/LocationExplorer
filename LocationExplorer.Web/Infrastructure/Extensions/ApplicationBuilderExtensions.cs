namespace LocationExplorer.Web.Infrastructure.Extensions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Data.Enumerations;
    using Domain.Models;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static IWebHost MigrateAndSeedDatabase(this IWebHost webhost)
        {
            using (var serviceScope = webhost.Services.CreateScope().ServiceProvider.CreateScope())
            {
                serviceScope.ServiceProvider.GetService<LocationExplorerDbContext>().Database.Migrate();

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                Task
                    .Run(async () =>
                    {
                        foreach (var role in Enum.GetValues(typeof(UserRoleEnum)).Cast<UserRoleEnum>())
                        {
                            var roleName = role.ToString();

                            var roleExists = await roleManager.RoleExistsAsync(roleName);

                            if (!roleExists)
                            {
                                await roleManager.CreateAsync(new IdentityRole(roleName));
                            }

                            if (role == UserRoleEnum.Administrator)
                            {
                                var adminEmail = "admin@mail.com";
                                var adminUserName = "admin";
                                var adminPass = "admin123";

                                var adminUser = await userManager.FindByEmailAsync(adminEmail);

                                if (adminUser == null)
                                {
                                    adminUser = new User { Email = adminEmail, UserName = adminUserName, FirstName = adminUserName, Birthday = DateTime.Today };

                                    await userManager.CreateAsync(adminUser, adminPass);

                                    await userManager.AddToRoleAsync(adminUser, roleName);
                                }
                            }
                        }
                    })
                    .Wait();
            }

            return webhost;
        }
    }
}