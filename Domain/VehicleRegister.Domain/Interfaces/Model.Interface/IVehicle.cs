using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRegister.Domain.Interfaces.Model.Interface
{
    public interface IVehicle
    {
        int Id { get; set; }
        int RegisterNumber { get; set; }
        string Model { get; set; }
        string Brand { get; set; }
        int Weight { get; set; }
        DateTime InTraffic { get; set; }
        bool DrivingBan { get; set; }
        bool ServiceBooked { get; set; }
        DateTime ServiceDate { get; set; }
        int YearlyFee { get; set; }
    }
}
