using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Interfaces.Service.Interface;

namespace VehicleRegister.Domain.Interfaces.Repository.Interface
{
    public interface IRepositoryWrapper
    {
        IVehicleRepository VehicleRepo { get; }
        IAutoMotiveRepairRepository RepairRepo { get; }
        IServiceReservationsRepository ServiceRepo { get; }
        IVehicleServiceHistoryRepository VehicleHistoryRepo { get; }
    }
}
