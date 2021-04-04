using EntityFramework.Data.Data;
using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Interfaces.Repository.Interface;
using VehicleRegister.Repository;

namespace VehicleRegister.Business.Wrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private IVehicleRepository _vehicle;
        private IServiceReservationsRepository _service;
        private IAutoMotiveRepairRepository _autoMotive;
        private readonly VehicleRegisterContext _entityDb;


        public IVehicleRepository VehicleRepo
        {
            get
            {
                if (_vehicle == null)
                {
                    _vehicle = new DatabaseRepository(_entityDb);
                }
                return _vehicle;
            }
        }

        public IAutoMotiveRepairRepository RepairRepo
        {
            get
            {
                if (_autoMotive == null)
                {
                    _autoMotive = new DatabaseRepository(_entityDb);
                }
                return _autoMotive;
            }
        }

        public IServiceReservationsRepository ServiceRepo
        {
            get
            {
                if (_service == null)
                {
                    _service = new DatabaseRepository(_entityDb);
                }
                return _service;
            }
        }

        public RepositoryWrapper(VehicleRegisterContext entityDb)
        {
            _entityDb = entityDb;
        }
    }
}

