using EntityFramework.Data.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Security.Claims;
using VehicleRegister.Domain.AppSettingsModels;

namespace VehicleRegister.CarAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            try
            {
                using (var scope = host.Services.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<VehicleRegisterContext>();
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();


                    context.Database.EnsureCreated();

                    if (!context.Users.Any())
                    {
                        var adminUser = new IdentityUser()
                        {
                            UserName = "Admin"
                        };

                        var managerUser = new IdentityUser()
                        {
                            UserName = "Manager"
                        };

                                        
                        userManager.CreateAsync(adminUser, AppSettings.AdminPassword).GetAwaiter().GetResult();
                        userManager.CreateAsync(managerUser, AppSettings.ManagerPassword).GetAwaiter().GetResult();

                        var adminClaim = new Claim("Role", "Admin");
                        var managerClaim = new Claim("Role", "Manager");

                        userManager.AddClaimAsync(adminUser, adminClaim).GetAwaiter().GetResult();
                        userManager.AddClaimAsync(managerUser, managerClaim).GetAwaiter().GetResult();

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
