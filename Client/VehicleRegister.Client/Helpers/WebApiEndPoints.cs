using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRegister.Client
{
    public static class WebApiEndPoints
    {
        private static string HostName => ConfigurationManager.AppSettings["HostName"];
        private static string RootBase = HostName + "api";

        public static class Vehicle
        {
            public static string GetVehicles = RootBase + "/Vehicles";
            public static string GetVehicle = RootBase + "/Vehicle";
        }
    }
}
