using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Domain.DTO.ReservationsDTO.Request;
using VehicleRegister.Domain.DTO.ReservationsDTO.Response;
using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Interfaces.Repository.Interface;
using VehicleRegister.Domain.Interfaces.Service.Interface;
using VehicleRegister.Domain.Models;

namespace VehicleRegister.Business.Service
{
    public class ServiceReservationsService : IServiceReservationService
    {

        private readonly IRepositoryWrapper _repo;
        public ServiceReservationsService(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        public async Task<bool> BookService(CreateReservationRequest request)
        {
            var vehicle = await _repo.VehicleRepo.GetVehicleById(request.VehicleId);

            if (vehicle == null) return false;
            
            var autoMotive = await _repo.RepairRepo.GetAutoMotive(request.AutoMotiveId);

            if (autoMotive == null) return false;


            var reservation = new ServiceReservations
            {
                VehicleId = vehicle.Id,
                AutoMotiveRepairId = autoMotive.Id,
                Date = request.Date
            };
    
          var created = await _repo.ServiceRepo.CreateReservations(reservation);

              if (created)
              {
                 vehicle.ServiceDate = request.Date;
                 vehicle.IsServiceBooked = true;
                    if (await _repo.VehicleRepo.UpdateVehicle(vehicle))
                    {
                       return true;
                    }  
              }
            return false;     
        }

        public async Task<bool> DeleteReservation(int id)
        {
            var reservation = await _repo.ServiceRepo.GetReservation(id);

            if (reservation == null) return false;

            if (await _repo.ServiceRepo.DeleteReservation(reservation))
                  return true;

            return false;
        }

        public async Task<bool> DeleteReservations()
        {
            var reservations = await _repo.ServiceRepo.GetAllReservations();
            var oldReservations = new List<IServiceReservations>();

            const int reservationsToDeleteDays = -30;

            foreach (var reserv in reservations.Where(x => x.Date < DateTime.Today.AddDays(reservationsToDeleteDays)))
            {
                oldReservations.Add(reserv);
            }
           
            var deleted = await _repo.ServiceRepo.DeleteAllReservations(oldReservations);

            if (deleted)
            {
                await SetDeleteReservations(oldReservations);
                return true;
            }
            return false;
        }

        private async Task SetDeleteReservations(List<IServiceReservations> oldReservations)
        {
            try
            {           
                var vehicles = new List<IVehicle>();
                foreach (var vehicleId in oldReservations.Select(x => x.VehicleId))
                {
                  var vehicle = await _repo.VehicleRepo.GetVehicleById(vehicleId);
                  vehicle.IsServiceBooked = false;
                  await _repo.VehicleRepo.UpdateVehicle(vehicle);
                }              
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public async Task<IEnumerable<IServiceReservations>> GetAllReservations() => await _repo.ServiceRepo.GetAllReservations();


        public async Task<IServiceReservations> GetReservation(int id) =>  await _repo.ServiceRepo.GetReservation(id);



        public async Task<UpdatedReservationResponse> UpdateServiceReservation(UpdateReservationRequest request)
        {
            var reservation = await _repo.ServiceRepo.GetReservation(request.Id);

            if (reservation == null) return null;

            reservation.AutoMotiveRepairId = request.AutoMotiveId;
            reservation.VehicleId = request.VehicleId;
            reservation.Date = request.Date;

            if (await _repo.ServiceRepo.UpdateReservations(reservation))
            {

                var autoMotiveId = await _repo.RepairRepo.GetAutoMotive(reservation.AutoMotiveRepairId);
                var vehicle = await _repo.VehicleRepo.GetVehicleById(reservation.VehicleId);

                return new UpdatedReservationResponse()
                {
                    Id = reservation.Id,
                    VehicleId = reservation.VehicleId,
                    AutoMotiveName = autoMotiveId.Name,
                    Date = reservation.Date,
                    RegisterNumber = vehicle.RegisterNumber
                };
            }
            return null;
        }
    }
}
