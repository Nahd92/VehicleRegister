using System;
using System.Collections.Generic;
using System.Text;
using VehicleRegister.Business.Service;
using VehicleRegister.Domain.Interfaces.Repository.Interface;
using VehicleRegister.Domain.Interfaces.Service.Interface;

namespace VehicleRegister.Business.Wrapper
{
    public class ServiceWrapper : IServiceWrapper
    {
        private IVehicleService _vehicle;
        private IRepositoryWrapper _wrapper;
        private IAutoMotiveRepairService _AutoMotiveRepair;
        private IServiceReservationService _service;

        public IVehicleService Vehicle
        {
            get
            {
                if (_vehicle == null)
                {
                    _vehicle = new VehicleService(_wrapper);
                }
                return _vehicle;
            }
        }

        public IAutoMotiveRepairService RepairService
        {
            get
            {
                if (_AutoMotiveRepair == null)
                {
                    _AutoMotiveRepair = new AutoMotiveRepairService(_wrapper);
                }
                return _AutoMotiveRepair;
            }
        }

        public IServiceReservationService ServiceReservations
        {
            get
            {
                if (_service == null)
                {
                    _service = new ServiceReservationsService(_wrapper);
                }
                return _service;
            }
        }

        public ServiceWrapper(IRepositoryWrapper wrapper)
        {
            _wrapper = wrapper;
        }
    }
}
