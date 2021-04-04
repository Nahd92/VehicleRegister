using EntityFramework.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VehicleRegister.Business.Wrapper;
using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Interfaces.Repository.Interface;
using VehicleRegister.Domain.Interfaces.Service.Interface;
using VehicleRegister.Repository;

namespace VehicleRegister.CarAPI.Helper
{
    public static class StartupExtension
    {

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<VehicleRegisterContext>(o => o.UseSqlServer(config.GetConnectionString("VehicleRegister")));
        }

        public static void ConfigureInjections(this IServiceCollection service)
        {
            service.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            service.AddScoped<IServiceWrapper, ServiceWrapper>();

            service.AddScoped<IVehicleRepository, DatabaseRepository>();
            service.AddScoped<IAutoMotiveRepairRepository, DatabaseRepository>();
            service.AddScoped<IServiceReservationsRepository, DatabaseRepository>();
        }
    }
}
