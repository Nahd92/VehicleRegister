using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Domain.DTO.ReservationsDTO.Request;
using VehicleRegister.Domain.DTO.ReservationsDTO.Response;
using VehicleRegister.Domain.Interfaces.Extensions.Interface;
using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Interfaces.Repository.Interface;
using VehicleRegister.Domain.Interfaces.Service.Interface;
using VehicleRegister.Domain.Models;

namespace VehicleRegister.Business.Service
{
    public class ServiceReservationsService : IServiceReservationService
    {

        private readonly IRepositoryWrapper _repo;
        private readonly ISpecialLoggerExtension _logger;
        public ServiceReservationsService(IRepositoryWrapper repo, ISpecialLoggerExtension logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<bool> BookService(CreateReservationRequest request)
        {
            var method = _logger.GetActualAsyncMethodName();
            try
            {
                var vehicle = await _repo.VehicleRepo.GetVehicleById(request.VehicleId);
                var autoMotive = await _repo.RepairRepo.GetAutoMotive(request.AutoMotiveRepairId);
                
                if (vehicle != null && autoMotive != null)
                {
                    _logger.LogInfo(GetType().Name, method, $"{vehicle.Id} was fetched from database");
                    _logger.LogInfo(GetType().Name, method, $"{autoMotive.Id} was fetched from database");

                    var reservation = new ServiceReservations
                    {
                        VehicleId = vehicle.Id,
                        AutoMotiveRepairId = autoMotive.Id,
                        Date = request.Date
                    };

                    var created = await _repo.ServiceRepo.CreateReservations(reservation);
                    if (created)
                    {
                        _logger.LogInfo(GetType().Name, method, "Created Vehicle returns true");
                        vehicle.ServiceDate = request.Date;
                        vehicle.IsServiceBooked = true;
                        if (await _repo.VehicleRepo.UpdateVehicle(vehicle))
                        {
                            _logger.LogInfo(GetType().Name, method, "Vehicle is updated");
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(GetType().Name, ex, method);
            }
            return false;
        }

        public async Task<bool> DeleteReservation(int id)
        {
            var method = _logger.GetActualAsyncMethodName();
            try
            {
                var reservation = await _repo.ServiceRepo.GetReservation(id);

                if (reservation != null)
                {
                    _logger.LogInfo(this.GetType().Name, method, $"{reservation} was fetched from database if exist");
                   var addedToServiceHistory = await _repo.VehicleHistoryRepo.AddOldServiceToHistory(new VehicleServiceHistory
                    {
                        ServiceDate = reservation.Date,
                        VehicleId = reservation.VehicleId,
                        AutoMotiveRepairId = reservation.AutoMotiveRepairId,
                    });

                    if (addedToServiceHistory)
                    {
                        var deleted = await _repo.ServiceRepo.DeleteReservation(reservation);

                        if (deleted)
                        {
                            var vehicle = await _repo.VehicleRepo.GetVehicleById(reservation.VehicleId);
                            vehicle.IsServiceBooked = false;
                            await _repo.VehicleRepo.UpdateVehicle(vehicle);
                            return true;
                        }
                    }                          
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(this.GetType().Name, ex, method);
           }
            return false;
        }

        public async Task<bool> DeleteReservations()
        {
            var method = _logger.GetActualAsyncMethodName();
            try
            {
                var reservations = await _repo.ServiceRepo.GetAllReservations();

                if (reservations != null)
                {
                    _logger.LogInfo(GetType().Name, method, $"Number of Reservatios fetched: {reservations.Count()}");

                    var oldReservations = new List<IServiceReservations>();

                    const int reservationsToDeleteDays = -30;
                    foreach (var reserv in reservations.Where(x => x.Date < DateTime.Today.AddDays(reservationsToDeleteDays)))
                    {
                        oldReservations.Add(reserv);
                    }
         
                await AddOldServicesToServiceHistory(oldReservations);
                _logger.LogInfo(GetType().Name, method, $"{oldReservations} was added to ServiceHistory");

                var IsDeleted = await _repo.ServiceRepo.DeleteAllReservations(oldReservations);

                    if (IsDeleted)
                    {
                        _logger.LogInfo(GetType().Name, method, "All oldreservations is deleted"); 
                        await SetDeleteReservations(oldReservations);
                        _logger.LogInfo(GetType().Name, method, "All vehicles which had service is now reset");

                       return true;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(GetType().Name, ex, method);
            }
            return false;
        }

        private async Task AddOldServicesToServiceHistory(List<IServiceReservations> oldReservations)
        {
            var method = _logger.GetActualAsyncMethodName();
            try
            {
                if (oldReservations.Count() != 0)
                {
                    var serviceHistory = new List<IVehicleServiceHistory>();
                    foreach (var service in oldReservations)
                    {
                        serviceHistory.Add(new VehicleServiceHistory
                        {
                            ServiceDate = service.Date,
                            AutoMotiveRepairId = service.AutoMotiveRepairId,
                            VehicleId = service.VehicleId
                        });
                    }

                    await _repo.VehicleHistoryRepo.AddOldServicesToHistory(serviceHistory);
                    _logger.LogInfo(GetType().Name, method, "All old Services added to database");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(GetType().Name, ex, method);
            }
          
        }

        private async Task SetDeleteReservations(List<IServiceReservations> oldReservations)
        {
            var method = _logger.GetActualAsyncMethodName();
            try
            {           
                var vehicles = new List<IVehicle>();
                foreach (var vehicleId in oldReservations.Select(x => x.VehicleId))
                {
                  var vehicle = await _repo.VehicleRepo.GetVehicleById(vehicleId);
                  vehicle.IsServiceBooked = false;
                  var IsUpdated = await _repo.VehicleRepo.UpdateVehicle(vehicle);

                    if (IsUpdated)
                    {
                        _logger.LogInfo(GetType().Name, method, "All vehicles have been updated");
                    }            
                }              
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(GetType().Name, ex, method);
            }
           
        }

        public async Task<IEnumerable<IServiceReservations>> GetAllReservations()
        {
            var method = _logger.GetActualAsyncMethodName();
            try
            {
                var reservations = await _repo.ServiceRepo.GetAllReservations();

                if (reservations.Count() != 0)
                {
                    _logger.LogInfo(GetType().Name, method, $"returns {reservations.Count()} from database");
                    return reservations;
                }
            }
            catch (Exception ex)
            {
              _logger.LogError(GetType().Name, ex, method);
            }
            return null;
        }

        public async Task<IServiceReservations> GetReservation(int id)
        {
            var method = _logger.GetActualAsyncMethodName();
            try
            {
                var reservation = await _repo.ServiceRepo.GetReservation(id);

                if (reservation != null)
                {
                    _logger.LogInfo(GetType().Name, method, $"Specific reservation was fetched");
                    return reservation;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(GetType().Name, ex, method);
            }
            return null;
        }

        public async Task<UpdatedReservationResponse> UpdateServiceReservation(UpdateReservationRequest request)
        {
            var method = _logger.GetActualAsyncMethodName();
            try
            {
                var reservation = await _repo.ServiceRepo.GetReservation(request.Id);

                if (reservation != null)
                {
                    _logger.LogInfo(GetType().Name, method, $"A reservation was fetched from database");
                    reservation.AutoMotiveRepairId = request.AutoMotiveId;
                    reservation.VehicleId = request.VehicleId;
                    reservation.Date = request.Date;

                    if (await _repo.ServiceRepo.UpdateReservations(reservation))
                    {
                        _logger.LogInfo(GetType().Name, method, $"Update was succesfull");

                        var autoMotiveId = await _repo.RepairRepo.GetAutoMotive(reservation.AutoMotiveRepairId);
                        var vehicle = await _repo.VehicleRepo.GetVehicleById(reservation.VehicleId);
                        if (autoMotiveId != null && vehicle != null)
                        {
                            _logger.LogInfo(GetType().Name, method, $"Gets an autoMotive from database");

                            return new UpdatedReservationResponse()
                            {
                                Id = reservation.Id,
                                VehicleId = reservation.VehicleId,
                                AutoMotiveName = autoMotiveId.Name,
                                Date = reservation.Date,
                                IsCompleted = reservation.IsCompleted,
                                RegisterNumber = vehicle.RegisterNumber,
                            };
                        }               
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(GetType().Name, ex, method);
                return null;
            }  
            return null;
        }

        public async Task<IEnumerable<IVehicleServiceHistory>> GetAllServiceHistories()
        {
            var method = _logger.GetActualAsyncMethodName();
            var reservationHistory = await _repo.VehicleHistoryRepo.VehicleHistory();

            if (reservationHistory.Count() != 0)
            {
                _logger.LogInfo(GetType().Name, method, $"returns {reservationHistory.Count()} serviceHistory!");
                return reservationHistory;
            }
            return null;
        }
    }
}
