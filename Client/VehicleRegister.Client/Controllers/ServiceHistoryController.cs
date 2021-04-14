using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
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
using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Models;
using VehicleRegister.Domain.Models.Auth;

namespace VehicleRegister.Client.Controllers
{
    public class ServiceHistoryController : Controller
    {
        public async Task<IActionResult> Index()
        {

            var listOfVehicles = new List<GetAllVehiclesDto>();
            var listOfAutoMotives = new List<GetAllAutoMotivesDto>();
            var serviceList = new List<ServiceReservationDto>();
            var historyList = new List<VehicleServiceHistory>();

            using (var _httpClient = new HttpClient())
            {

                var session = SessionHelper.GetObjectFromJson<LoginModel>(HttpContext.Session, "identity");

                if (session is null) return RedirectToAction("Login", "Account");

                var requestUrl = VehicleRoute.Vehicles;
                var response = await _httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    listOfVehicles = JsonConvert.DeserializeObject<List<GetAllVehiclesDto>>(jsonString);
                }

                var Url = AutoMotiveRoute.AutoMotives;
                var responsed = await _httpClient.GetAsync(Url);
                if (responsed.IsSuccessStatusCode)
                {
                    var jsonString = responsed.Content.ReadAsStringAsync().Result;
                    listOfAutoMotives = JsonConvert.DeserializeObject<List<GetAllAutoMotivesDto>>(jsonString);
                }


                var serviceListUrl = ServiceHistory.Reservations;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.Token);
                var serviceResponse = await _httpClient.GetAsync(serviceListUrl);
                if (serviceResponse.IsSuccessStatusCode)
                {
                    var jsonString = serviceResponse.Content.ReadAsStringAsync().Result;
                    serviceList = JsonConvert.DeserializeObject<List<ServiceReservationDto>>(jsonString);
                }


                var serviceHistoryUrl = ServiceHistory.ServiceHistories;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.Token);
                var serviceHistoryResponse = await _httpClient.GetAsync(serviceHistoryUrl);
                if (serviceHistoryResponse.IsSuccessStatusCode)
                {
                    var jsonString = serviceHistoryResponse.Content.ReadAsStringAsync().Result;
                    historyList = JsonConvert.DeserializeObject<List<VehicleServiceHistory>>(jsonString);
                }
            }

            
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

            return View(listOfNewGetAllReservationDto);
        }



        [HttpGet]
        public async Task<IActionResult> ClearServiceHistory()
        {
            using (var _httpClient = new HttpClient())
            {
                var session = SessionHelper.GetObjectFromJson<LoginModel>(HttpContext.Session, "identity");

                if (session is null) return RedirectToAction("Login", "Account");

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.Token);
                var requestUrl = ServiceHistory.DeleteReservations;
                var response = await _httpClient.DeleteAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("index");
                }
            }
            return RedirectToAction("Index", "Home");
        }



        [HttpGet]
        public async Task<IActionResult> Completed(int id)
        {
            using (var _httpClient = new HttpClient())
            {
                var session = SessionHelper.GetObjectFromJson<LoginModel>(HttpContext.Session, "identity");

                if (session is null) return RedirectToAction("Login", "Account");

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.Token);
                var requestUrl = ServiceHistory.DeleteReservation + id;
                var response = await _httpClient.DeleteAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("index");
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
