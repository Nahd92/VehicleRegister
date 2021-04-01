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



        public ServiceWrapper(IRepositoryWrapper wrapper)
        {
            _wrapper = wrapper;
        }
    }
}
