using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using VehicleRegister.Client.Helpers;
using VehicleRegister.Domain.DTO.AutoMotiveDTO.Response;
using VehicleRegister.Domain.DTO.ReservationsDTO;
using VehicleRegister.Domain.DTO.ReservationsDTO.Response;
using VehicleRegister.Domain.DTO.VehicleDTO.Response;
using VehicleRegister.Domain.Interfaces.Client.Service.Interface;
using VehicleRegister.Domain.Models;
using VehicleRegister.Domain.Models.Auth;

namespace VehicleRegister.Client.Business
{
    public class ServiceHistory : IServiceHistory
    {

        public List<GetAllReservationsDto> AddAllListToOne(List<GetAllVehiclesDto> listOfVehicles, List<GetAllAutoMotivesDto> listOfAutoMotives, List<ServiceReservationDto> serviceList, List<VehicleServiceHistory> historyList)
        {
            var listOfNewGetAllReservationDto = new List<GetAllReservationsDto>();
            foreach (var item in serviceList)
            {
                listOfNewGetAllReservationDto.Add(new GetAllReservationsDto()
                {
                    Id = item.Id,
                    Date = item.Date,
                    AutoMotivesName = listOfAutoMotives.Where(x => x.Id == item.AutoMotiveRepairId).Select(x => x.Name).FirstOrDefault(),
                    VehiclesRegisterNumber = listOfVehicles.Where(x => x.Id == item.VehicleId).Select(x => x.RegisterNumber).FirstOrDefault(),
                    IsCompleted = item.IsCompleted
                });
            }


            if (serviceList.Count() == 0 || historyList.Count() != 0)
            {
                foreach (var item in historyList)
                {
                    listOfNewGetAllReservationDto.Add(new GetAllReservationsDto(new VehicleServiceHistory
                    {
                        Id = item.Id,
                        ServiceDate = item.ServiceDate,
                        VehicleId = item.VehicleId,
                        AutoMotiveRepairId = item.AutoMotiveRepairId
                    })
               );
                };
            }

            return listOfNewGetAllReservationDto;
        }

        public  async Task<List<GetAllVehiclesDto>> GetAllVehicles(List<GetAllVehiclesDto> listOfVehicles, HttpClient _httpClient)
        {
            var requestUrl = VehicleRoute.Vehicles;
            var response = await _httpClient.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                listOfVehicles = JsonConvert.DeserializeObject<List<GetAllVehiclesDto>>(jsonString);
            }

            return listOfVehicles;
        }

        public async Task<List<GetAllAutoMotivesDto>> GetAllAutoMotives(List<GetAllAutoMotivesDto> listOfAutoMotives, HttpClient _httpClient)
        {
            var Url = AutoMotiveRoute.AutoMotives;
            var responsed = await _httpClient.GetAsync(Url);
            if (responsed.IsSuccessStatusCode)
            {
                var jsonString = responsed.Content.ReadAsStringAsync().Result;
                listOfAutoMotives = JsonConvert.DeserializeObject<List<GetAllAutoMotivesDto>>(jsonString);
            }

            return listOfAutoMotives;
        }

        public  async Task<List<ServiceReservationDto>> GetAllServices(List<ServiceReservationDto> serviceList, HttpClient _httpClient, LoginModel session)
        {
            var serviceListUrl = ServiceHistorys.Reservations;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.Token);
            var serviceResponse = await _httpClient.GetAsync(serviceListUrl);
            if (serviceResponse.IsSuccessStatusCode)
            {
                var jsonString = serviceResponse.Content.ReadAsStringAsync().Result;
                serviceList = JsonConvert.DeserializeObject<List<ServiceReservationDto>>(jsonString);
            }

            return serviceList;
        }

        public  async Task<List<VehicleServiceHistory>> GetAllHistory(List<VehicleServiceHistory> historyList, HttpClient _httpClient, LoginModel session)
        {
            var serviceHistoryUrl = ServiceHistorys.ServiceHistories;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.Token);
            var serviceHistoryResponse = await _httpClient.GetAsync(serviceHistoryUrl);
            if (serviceHistoryResponse.IsSuccessStatusCode)
            {
                var jsonString = serviceHistoryResponse.Content.ReadAsStringAsync().Result;
                historyList = JsonConvert.DeserializeObject<List<VehicleServiceHistory>>(jsonString);
            }

            return historyList;
        }
    }
}
