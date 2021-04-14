using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleRegister.Client.Business;
using VehicleRegister.Client.Helpers;
using VehicleRegister.Domain.AppSettingsModels;
using VehicleRegister.Domain.Interfaces.Client.Service.Interface;

namespace VehicleRegister.Client.ServiceHelper
{
    public static class ServiceExtension
    {

        public static void ConfigureInjections(this IServiceCollection service)
        {
            service.AddScoped<IServiceHistory, ServiceHistory>();

        }



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
