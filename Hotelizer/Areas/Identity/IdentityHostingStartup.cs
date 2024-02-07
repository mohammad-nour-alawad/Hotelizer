using System;
using DBConnection.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Hotelizer.Areas.Identity.IdentityHostingStartup))]
namespace Hotelizer.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                //services.AddDbContext<fiveSeasonDBContext>(options =>
                //options.UseSqlServer(
                //    context.Configuration.GetConnectionString("DefaultConnection")
                //    )
                //    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                //);

                
                
                //services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                //    .AddRoles<IdentityRole>()
                //    .AddUserManager<UserManager<ApplicationUser>>()
                //    .AddRoleManager<RoleManager<IdentityRole>>()
                //    .AddSignInManager<SignInManager<ApplicationUser>>()
                //    .AddEntityFrameworkStores<fiveSeasonDBContext>();

                
            });
        }
    }
}