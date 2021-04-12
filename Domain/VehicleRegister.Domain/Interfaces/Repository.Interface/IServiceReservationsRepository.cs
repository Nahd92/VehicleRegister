using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Domain.DTO.ReservationsDTO.Request;
using VehicleRegister.Domain.Models;

namespace VehicleRegister.Domain.Interfaces.Model.Interface
{
    public interface IServiceReservationsRepository
    {
        Task<IEnumerable<IServiceReservations>> GetAllReservations();
        Task<IServiceReservations> GetReservation(int id);
        Task<bool> CreateReservations(IServiceReservations reservation);
        Task<bool> UpdateReservations(IServiceReservations request);
        Task<bool> DeleteAllReservations(List<IServiceReservations> reservations);
        Task<bool> DeleteReservation(IServiceReservations reservations);
    }
}
