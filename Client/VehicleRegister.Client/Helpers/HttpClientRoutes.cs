using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using VehicleRegister.Domain.AppSettingsModels;

namespace VehicleRegister.Client.Helpers
{
    public static class HttpClientRoutes
    {
        public static string HostName(this IConfiguration configuration) => configuration.GetValue<string>("Url:HostName");

        public static class IdentityRoute
        {
            public static string Token => AppSettings.HostName + "api/Token";
            public static string Register => AppSettings.HostName + "api/Register";

        }

        public static class VehicleRoute
        {
            public static string Vehicles = AppSettings.HostName + "api/Vehicles";
            public static string VehicleByRegisterName = AppSettings.HostName + "api/VehicleByKeyword/";
            public static string GetVehicle = AppSettings.HostName + "api/Vehicle/{id}";
            public static string CreateVehicle = AppSettings.HostName + "api/Vehicle";
            public static string UpdateVehicle = AppSettings.HostName + "api/Vehicle/{id}";
            public static string DeleteVehicle = AppSettings.HostName = "api/Vehicle/{id}";
        }
    }
}