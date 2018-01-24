using System;
using System.Threading.Tasks;
using fullPlate.Data;
using fullPlate.Data.Models;
using fullPlate.Services;
using fullPlate.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
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

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    // Cookie settings
                    options.Cookie.HttpOnly = false;
                    options.Cookie.Expiration = TimeSpan.FromDays(150);
                    options.LoginPath = "/api/user/login";
                    options.LogoutPath = "/api/user/logout";
                    options.SlidingExpiration = true;
                    options.Events.OnRedirectToLogin = (context) =>
                    {
                        context.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    };
                });

            services.AddIdentity<User, IdentityRole>()
              .AddEntityFrameworkStores<FullPlateContext>()
              .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 2;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 2;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });
            
            services.AddTransient<IRestaurantsService, RestaurantService>();
            services.AddTransient<ILunchService, LunchService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<DataSeeder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DataSeeder seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();

                // Setup WebpackDevMidleware for "Hot module replacement" while debugging
                var options = new WebpackDevMiddlewareOptions() { HotModuleReplacement = true };
                app.UseWebpackDevMiddleware(options);

                seeder.SeedRoles().Wait();
                seeder.SeedUsers().Wait();
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
