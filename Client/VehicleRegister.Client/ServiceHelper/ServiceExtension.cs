using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleRegister.Client.Helpers;
using VehicleRegister.Domain.AppSettingsModels;

namespace VehicleRegister.Client.ServiceHelper
{
    public static class ServiceExtension
    {

        public static void ConfigureSession(this IServiceCollection service)
        {
            service.AddSession(opt =>
            {
                opt.IdleTimeout = TimeSpan.FromHours(2);
                opt.Cookie.HttpOnly = true;
                opt.Cookie.IsEssential = true;
            });
        }

        public static void ConfigureAppsettingsValuesInjection(this IServiceCollection service, IConfiguration configuration)
        {
            AppSettings.HostName = HttpClientRoutes.HostName(configuration);
        }
    }
}
