using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fullPlate.Data;
using fullPlate.Data.Models;
using fullPlate.Services;
using fullPlate.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace fullPlate
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<FullPlateContext>(options =>
              options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
              .AddEntityFrameworkStores<FullPlateContext>()
              .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
              // Password settings
              options.Password.RequireDigit = true;
              options.Password.RequiredLength = 8;
              options.Password.RequireNonAlphanumeric = false;
              options.Password.RequireUppercase = true;
              options.Password.RequireLowercase = false;
              options.Password.RequiredUniqueChars = 6;

              // Lockout settings
              options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
              options.Lockout.MaxFailedAccessAttempts = 10;
              options.Lockout.AllowedForNewUsers = true;

              // User settings
              options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
              // Cookie settings
              options.Cookie.HttpOnly = true;
              options.Cookie.Expiration = TimeSpan.FromDays(150);
              options.SlidingExpiration = true;
            });

            services.AddTransient<IRestaurantsService, RestaurantService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();

                // Setup WebpackDevMidleware for "Hot module replacement" while debugging
                var options = new WebpackDevMiddlewareOptions() { HotModuleReplacement = true };
                app.UseWebpackDevMiddleware(options);
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");


                // Setup additional routing for SPA
                routes.MapSpaFallbackRoute(
                  name: "spa-fallback",
                  defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
