using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NicksUsedCars.Models
{
    public class NicksUsedCarsContext : IdentityDbContext<IdentityUser>
    {
        public NicksUsedCarsContext(DbContextOptions<NicksUsedCarsContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<IdentityUser>().Property(typeof(string), "FirstName").IsRequired(true).HasMaxLength(30);
            builder.Entity<IdentityUser>().Property(typeof(string), "LastName").IsRequired(true).HasMaxLength(30);
        }
    }
}
