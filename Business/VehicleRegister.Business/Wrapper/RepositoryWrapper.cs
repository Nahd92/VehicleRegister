using EntityFramework.Data.Data;
using VehicleRegister.Domain.Interfaces.Logger.Interface;
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
        private readonly ILoggerManager _logger;

        public IVehicleRepository VehicleRepo
        {
            get
            {
                if (_vehicle == null)
                {
                    _vehicle = new DatabaseRepository(_entityDb, _logger);
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
                    _autoMotive = new DatabaseRepository(_entityDb, _logger);
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
                    _service = new DatabaseRepository(_entityDb, _logger);
                }
                return _service;
            }
        }


        public RepositoryWrapper(VehicleRegisterContext entityDb, ILoggerManager logger)
        {
            _entityDb = entityDb;
            _logger = logger;
        }
    }
}

