using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Domain.DTO.ReservationsDTO.Request;
using VehicleRegister.Domain.DTO.ReservationsDTO.Response;
using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Interfaces.Repository.Interface;
using VehicleRegister.Domain.Interfaces.Service.Interface;

namespace VehicleRegister.Business.Service
{
    public class ServiceReservationsService : IServiceReservationService
    {

        private readonly IRepositoryWrapper _repo;
        public ServiceReservationsService(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        public Task<bool> CreateReservation(CreateReservationRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteReservation(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IServiceReservations>> GetAllReservations()
        {
            throw new NotImplementedException();
        }

        public Task<IServiceReservationService> GetReservation()
        {
            throw new NotImplementedException();
        }

        public Task<UpdatedReservationResponse> UpdateReservation()
        {
            throw new NotImplementedException();
        }
    }
}
