using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using PerfectHomeToYou.Data;
using PerfectHomeToYou.Infrastructure;
using PerfectHomeToYou.Services.Cities;
using PerfectHomeToYou.Services.Apartments;
using PerfectHomeToYou.Services.Neighborhoods;
using PerfectHomeToYou.Services.Clients;
using PerfectHomeToYou.Data.Models;
using PerfectHomeToYou.Services.Statistics;
using PerfectHomeToYou.Services.Questions;

namespace PerfectHomeToYou
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
            => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PerfectHomeToYouDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<User>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<PerfectHomeToYouDbContext>();
            
            services.AddControllersWithViews();

            services.AddTransient<IApartmentService, ApartmentService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<INeighborhoodService, NeighborhoodService>();
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IStatisticsService, StatisticsService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultAreaRoute();

                endpoints.MapControllerRoute(
                    name: "Apartment Details",
                    pattern: "/Apartments/Details/{id}/{information}",
                    defaults: new { controller = "Apartments", action = "Details" });

                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
    }
}
