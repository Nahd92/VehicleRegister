using EntityFramework.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using VehicleRegister.Business.Service;
using VehicleRegister.Business.Wrapper;
using VehicleRegister.Domain.AppSettingsModels;
using VehicleRegister.Domain.Interfaces.Auth.Interface;
using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Interfaces.Repository.Interface;
using VehicleRegister.Domain.Interfaces.Service.Interface;
using VehicleRegister.Repository;
using VehicleRegister.VehicleAPI.Helper.AppsettingsHelper;

namespace VehicleRegister.CarAPI.Helper
{
    public static class ServiceExtension
    {

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<VehicleRegisterContext>(o => o.UseSqlServer(config.GetConnectionString("VehicleRegister")));
        }

        public static void ConfigureInjections(this IServiceCollection service)
        {
            service.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            service.AddScoped<IServiceWrapper, ServiceWrapper>();

            service.AddScoped<AuthenticationService>();

            service.AddScoped<IVehicleRepository, DatabaseRepository>();
            service.AddScoped<IAutoMotiveRepairRepository, DatabaseRepository>();
            service.AddScoped<IServiceReservationsRepository, DatabaseRepository>();
        }

        public static void ConfigureCors(this IServiceCollection service)
        {
            service.AddCors(opt =>
            {
                opt.AddPolicy("Cors", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        public static void ConfigureAppsettingsValuesInjection(this IServiceCollection service, IConfiguration configuration)
        {
            var settings = new AppSettings
            {
                SecretKey = AppSettingsHelper.SecretKey(configuration),
                HostName = AppSettingsHelper.HostName(configuration)
            };

            service.AddSingleton(settings);
        }

       
    }
}
