using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VehicleRegister.Domain.Models.Vehicles
{
    [Table("Vehicles")]
    public class LightVehicle : Vehicle
    {

        public LightVehicle()
        {

        }

        public LightVehicle(int id, string registerNumber, string brand, string model, DateTime inTraffic, bool isDrivingBan, bool isServiceBooked, DateTime serviceDate,  int weight, int yearlyFee)
        {
            this.Id = id;
            this.RegisterNumber = registerNumber;
            this.Brand = brand;
            this.Model = model;
            this.InTraffic = inTraffic;
            this.IsDrivingBan = isDrivingBan;
            this.IsServiceBooked = isServiceBooked;
            this.ServiceDate = serviceDate;
            this.Weight = weight;
            this.YearlyFee = CalculateYearlyFee();
        } 

        public override int CalculateYearlyFee()
        {
            if (this.Weight <= 1800)
            {
                this.YearlyFee = 1200;
            }
            return this.YearlyFee;
        }
    }
}
