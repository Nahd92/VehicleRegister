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
     
    }
    public static class IdentityRoute
    {
        public static string Token => AppSettings.HostName + "api/Token";
        public static string UserInformation => AppSettings.HostName + "api/UserInformation/";
        public static string Register => AppSettings.HostName + "api/Register";

    }

    public static class VehicleRoute
    {
        public static string Vehicles = AppSettings.HostName + "api/Vehicles";
        public static string VehicleByRegisterName = AppSettings.HostName + "api/VehicleByKeyword/";
        public static string GetVehicle = AppSettings.HostName + "api/Vehicle/";
        public static string CreateVehicle = AppSettings.HostName + "api/Vehicle";
        public static string DeleteVehicel = AppSettings.HostName + "api/Vehicle/";
        public static string UpdateVehicle = AppSettings.HostName + "api/Vehicle/";
    }
    public static class AutoMotiveRoute
    {
        public static string AutoMotives = AppSettings.HostName + "api/AutoMotives";
        public static string AutoMotive = AppSettings.HostName + "api/AutoMotive/";
        public static string CreateAutoMotive = AppSettings.HostName + "api/AutoMotive";
        public static string DeleteAutoMotive = AppSettings.HostName + "api/AutoMotive/";
        public static string UpdateAutoMotive = AppSettings.HostName + "api/AutoMotive/";
    }

    public static class ServiceHistory
    {
        public static string Reservations = AppSettings.HostName + "api/Reservations";
    }



    public static class ServiceRoute
    {
        public static string CreateBooking = AppSettings.HostName + "api/Reservation";
    }
}