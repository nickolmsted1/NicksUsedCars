using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NicksUsedCars.Models;

[assembly: HostingStartup(typeof(NicksUsedCars.Areas.Identity.IdentityHostingStartup))]
namespace NicksUsedCars.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<NicksUsedCarsContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("NicksUsedCarsContextConnection")));

                services.AddDefaultIdentity<ApplicationUser>()
                    .AddEntityFrameworkStores<NicksUsedCarsContext>();
            });
        }
    }
}