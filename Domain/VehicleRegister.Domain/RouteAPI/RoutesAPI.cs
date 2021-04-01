using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRegister.Domain.RouteAPI
{
    public static class RoutesAPI
    {
        public const string Root = "api";
        private const string Base = Root + "/";
        

        public class Vehicle
        {
            public const string VehicleController = Base + "Controller";
            public const string GetAllVehicles = Base + "Vehicles";
            public const string GetVehicle = Base + "Vehicle/{id}";
        }
    }
}
