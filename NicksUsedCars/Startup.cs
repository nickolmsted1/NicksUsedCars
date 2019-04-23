using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NicksUsedCars.Models;

namespace NicksUsedCars
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public class ApplicationUser : IdentityUser
        {

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddUserManager<IdentityUser>()
                    .AddEntityFrameworkStores<NicksUsedCarsContext>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        /*public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            ConfigureAsync(app, env, serviceProvider);
        }*/

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();

            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            await CreateRoles(serviceProvider);
        }

        

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            // initialize roles
            RoleManager<IdentityRole> RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>();
                //serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin", "Manager", "Employee", "Customer" };
            IdentityResult roleResult;

            foreach (string role in roleNames)
            {
                bool roleExist = await RoleManager.RoleExistsAsync(role);
                // ensure that the role does not exist
                if (!roleExist)
                {
                    // create role and seed to database
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // create super user
            var powerUser = new ApplicationUser
            {
                UserName = Configuration["UserSettings:UserName"],
                Email = Configuration["UserSettings:UserEmail"]
            };

            // Ensure admin login values are in appSettings.json
            string userPass = Configuration["UserSettings:UserPassword"];
            var _user = await UserManager.FindByEmailAsync(Configuration["UserSettings:UserEmail"]);
            if (_user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(powerUser, userPass);
                if (createPowerUser.Succeeded)
                {
                    // tie user to "Admin" role
                    await UserManager.AddToRoleAsync(powerUser, "Admin");
                }
            }
        }
    }
}
