﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VehicleRegister.Domain.Models.Vehicles
{
    [Table("Vehicles")]
    public class HeavyVehicle : Vehicle
    {

        public HeavyVehicle(int id, string registerNumber, string brand, string model, DateTime inTraffic, bool isDrivingBan, bool isServiceBooked, DateTime ServiceDate, int weight, int yearlyFee)
        {
            this.Id = id;
            this.RegisterNumber = registerNumber;
            this.Brand = brand;
            this.Model = model;
            this.InTraffic = inTraffic;
            this.IsDrivingBan = isDrivingBan;
            this.IsServiceBooked = isServiceBooked;
            this.ServiceDate = ServiceDate;
            this.Weight = weight;
            this.YearlyFee = CalculateYearlyFee();
        }

        public HeavyVehicle(int id, string registerNumber, string brand, string model, DateTime inTraffic, bool isDrivingBan, bool isServiceBooked,int weight, int yearlyFee)
        {
            this.Id = id;
            this.RegisterNumber = registerNumber;
            this.Brand = brand;
            this.Model = model;
            this.InTraffic = inTraffic;
            this.IsDrivingBan = isDrivingBan;
            this.IsServiceBooked = isServiceBooked;
            this.Weight = weight;
            this.YearlyFee = CalculateYearlyFee();
        }

        public override int CalculateYearlyFee()
        {
            if (Weight > 2500)
            {
                YearlyFee = 4500;
            }
            return YearlyFee;

        }
    }
}
