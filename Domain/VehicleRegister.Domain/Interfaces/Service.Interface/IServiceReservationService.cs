using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Domain.DTO.ReservationsDTO.Request;
using VehicleRegister.Domain.DTO.ReservationsDTO.Response;
using VehicleRegister.Domain.Interfaces.Model.Interface;

namespace VehicleRegister.Domain.Interfaces.Service.Interface
{
    public interface IServiceReservationService
    {
        Task<IEnumerable<IServiceReservations>> GetAllReservations();
        Task<IServiceReservations> GetReservation(int id);
        Task<bool> DeleteReservations();
        Task<bool> DeleteReservation(int id);
        Task<bool> CreateReservation(CreateReservationRequest request);
        Task<UpdatedReservationResponse> UpdateReservation(UpdateReservationRequest request);
    }
}
