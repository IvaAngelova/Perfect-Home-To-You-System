using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using PerfectHomeToYou.Data;
using PerfectHomeToYou.Data.Models;

using static PerfectHomeToYou.WebConstants;
using static PerfectHomeToYou.Areas.Admin.AdminConstants;

namespace PerfectHomeToYou.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
           this IApplicationBuilder app)
        {
            using var scopredServices = app.ApplicationServices.CreateScope();

            var serviceProvider = scopredServices.ServiceProvider;

            var data = scopredServices.ServiceProvider.GetService<PerfectHomeToYouDbContext>();

            SeedAdministrator(serviceProvider);

            data.Database.Migrate();

            return app;
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                {
                    return;
                }

                var role = new IdentityRole
                {
                    Name = AdministratorRoleName
                };

                await roleManager.CreateAsync(role);

                const string adminEmail = "admin@crs.com";
                const string adminPassword = "admin123";

                var user = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    FullName = "Admin"
                };

                await userManager.CreateAsync(user, adminPassword);

                await userManager.AddToRoleAsync(user, role.Name);
            })
                .GetAwaiter()
                .GetResult();
        }
    }
}
