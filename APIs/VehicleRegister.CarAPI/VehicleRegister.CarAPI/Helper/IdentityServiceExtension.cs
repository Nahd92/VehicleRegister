using EntityFramework.Data.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRegister.VehicleAPI.Helper
{
    public static class IdentityServiceExtension
    {

        public static void ConfigureBearer(this IServiceCollection service, IConfiguration Config)
        {
            service.AddAuthentication(cfg => 
            {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                    .AddJwtBearer(config =>
                    {
                        var secretBytes = Encoding.UTF8.GetBytes(Config["SecretKey:Key"]);
                        var key = new SymmetricSecurityKey(secretBytes);
                        config.Events = new JwtBearerEvents()
                        {
                            OnMessageReceived = context =>
                            {
                                if (context.Request.Cookies.ContainsKey("X-Access-Token"))
                                {
                                    context.Token = context.Request.Cookies["X-Access-Token"];
                                }
                                return Task.CompletedTask;
                            }
                        };

                         config.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer = true,
                            ValidIssuer = Config["Url:HostName"],
                            ValidateAudience = true,
                            ValidAudience = Config["Url:HostName"],
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = key,
                        };
                        
                        config.SaveToken = true;
                    });
        }

        public static void ConfigureIdentityOptions(this IServiceCollection service)
        {
            service.AddIdentityCore<IdentityUser>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<VehicleRegisterContext>();
        }

    }
}
