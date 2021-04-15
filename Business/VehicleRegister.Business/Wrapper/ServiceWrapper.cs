using EntityFramework.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleRegister.Business.Service;
using VehicleRegister.Domain.AppSettingsModels;
using VehicleRegister.Domain.Interfaces.Auth.Interface;
using VehicleRegister.Domain.Interfaces.Extensions.Interface;
using VehicleRegister.Domain.Interfaces.Logger.Interface;
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
        private IAuthenticationService _authService;
        private UserManager<IdentityUser> manager;
        private ISpecialLoggerExtension _logger;


        public IVehicleService Vehicle
        {
            get
            {
                if (_vehicle == null)
                {
                    _vehicle = new VehicleService(_wrapper, _logger);
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

        public IAuthenticationService authService 
        {
            get
            {
                if (_authService == null)
                {
                    _authService = new AuthenticationService(manager, _logger);
                }
                return _authService;
            }
        }

        public ServiceWrapper(IRepositoryWrapper wrapper, UserManager<IdentityUser> manager, ISpecialLoggerExtension logger)
        {
            _wrapper = wrapper;
            this.manager = manager;
            _logger = logger;
        }
    }
}
