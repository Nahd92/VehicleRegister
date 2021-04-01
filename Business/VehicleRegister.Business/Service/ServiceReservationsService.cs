using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
