using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using PerfectHomeToYou.Data;

namespace PerfectHomeToYou.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
           this IApplicationBuilder app)
        {
            using var scopredServices = app.ApplicationServices.CreateScope();

            var data = scopredServices.ServiceProvider.GetService<PerfectHomeToYouDbContext>();

            data.Database.Migrate();

            return app;
        }
    }
}
