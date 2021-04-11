using System;
using System.Collections.Generic;
using System.Text;
using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Models;
using VehicleRegister.Domain.Models.Vehicles;

namespace VehicleRegister.Domain.Factory
{
    public static class VehicleFactory 
    {

        public static IVehicle Create(int? id, string registerNumber, string brand, string model, DateTime inTraffic, bool isDrivingBan, bool isServiceBooked, int weight, int yearlyFee)
        {
            switch (weight)
            {
                case int n when (n <= 1800):
                    return new LightVehicle(id.Value, registerNumber, brand, model, inTraffic, isDrivingBan, isServiceBooked, weight, yearlyFee);
                case int b when (b > 1800 && b <= 2500):
                    return new MediumVehicle(id.Value, registerNumber, brand, model, inTraffic, isDrivingBan, isServiceBooked, weight, yearlyFee);
                default:
                    return new HeavyVehicle(id.Value, registerNumber, brand, model, inTraffic, isDrivingBan, isServiceBooked, weight, yearlyFee);
            }
        }
    }
 }
