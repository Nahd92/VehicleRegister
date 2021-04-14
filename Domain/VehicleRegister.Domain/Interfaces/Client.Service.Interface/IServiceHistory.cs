using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Domain.DTO.AutoMotiveDTO.Response;
using VehicleRegister.Domain.DTO.ReservationsDTO;
using VehicleRegister.Domain.DTO.ReservationsDTO.Response;
using VehicleRegister.Domain.DTO.VehicleDTO.Response;
using VehicleRegister.Domain.Models;
using VehicleRegister.Domain.Models.Auth;

namespace VehicleRegister.Domain.Interfaces.Client.Service.Interface
{
   public interface IServiceHistory
    {
        Task<List<VehicleServiceHistory>> GetAllHistory(List<VehicleServiceHistory> historyList, HttpClient _httpClient, LoginModel session);
        Task<List<ServiceReservationDto>> GetAllServices(List<ServiceReservationDto> serviceList, HttpClient _httpClient, LoginModel session);
        Task<List<GetAllAutoMotivesDto>> GetAllAutoMotives(List<GetAllAutoMotivesDto> listOfAutoMotives, HttpClient _httpClient);
        Task<List<GetAllVehiclesDto>> GetAllVehicles(List<GetAllVehiclesDto> listOfVehicles, HttpClient _httpClient);
        List<GetAllReservationsDto> AddAllListToOne(List<GetAllVehiclesDto> listOfVehicles, List<GetAllAutoMotivesDto> listOfAutoMotives, List<ServiceReservationDto> serviceList, List<VehicleServiceHistory> historyList);
    }
}
