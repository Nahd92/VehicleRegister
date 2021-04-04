using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRegister.Domain.Interfaces.Model.Interface
{
    public interface IVehicle
    {
        int Id { get; set; }
        string RegisterNumber { get; set; }
        string Model { get; set; }
        string Brand { get; set; }
        int Weight { get; set; }
        DateTime InTraffic { get; set; }
        bool IsDrivingBan { get; set; }
        bool IsServiceBooked { get; set; }
        DateTime ServiceDate { get; set; }
        int YearlyFee { get; set; }
    }
}
