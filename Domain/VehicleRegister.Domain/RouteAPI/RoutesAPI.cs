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
            public const string GetAllVehicles = Base + "Vehicles";
            public const string GetVehicle = Base + "Vehicle/{id}";
            public const string GetVehicleByKeyword = Base + "VehicleByKeyword/{keyword}";
            public const string CreateVehicle = Base + "Vehicle";
            public const string DeleteVehicle = Base + "Vehicle/{id}";
            public const string UpdateVehicle = Base + "Vehicle";
        }
        public class AutoMotive
        {
            public const string GetAllAutoMotives = Base + "AutoMotives";
            public const string GetAutoMotives = Base + "AutoMovie/{id}";
            public const string CreateAutoMotives = Base + "AutoMotives";
            public const string UpdateAutoMotive = Base + "AutoMotives";
        }
        public class Reservations
        {
            public const string GetAllServiceReservations = Base + "Reservations";
            public const string GetServiceReservation = Base + "Reservation/{id}";
            public const string BookServiceReservation = Base + "Reservation";
            public const string RemoveServiceReservations = Base + "Reservations";
            public const string RemoveServiceReservation = Base + "Reservation/{id}";
            public const string UpdateServiceReservation = Base + "Reservation";
        }
        public class Identity
        {
            public const string Token = Base + "Token";
            public const string Register = Base + "Register";
        }
    }
    
}
