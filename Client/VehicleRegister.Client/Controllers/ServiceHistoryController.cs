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
using VehicleRegister.Domain.Models.Auth;

namespace VehicleRegister.Client.Controllers
{
    public class ServiceHistoryController : Controller
    {
        public async Task<IActionResult> Index()
        {

            var session = SessionHelper.GetObjectFromJson<LoginModel>(HttpContext.Session, "identity");

            if (session is null) return RedirectToAction("Login", "Account");


            var listOfVehicles = new List<GetAllVehiclesDto>();
            var listOfAutoMotives = new List<GetAllAutoMotivesDto>();
            var listOfServiceHistory = new List<ServiceReservationDto>();

            using (var _httpClient = new HttpClient())
            {
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


                var serviceHistoryUrl = ServiceHistory.Reservations;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.Token);
                var serviceResponse = await _httpClient.GetAsync(serviceHistoryUrl);
                if (serviceResponse.IsSuccessStatusCode)
                {
                    var jsonString = serviceResponse.Content.ReadAsStringAsync().Result;
                    listOfServiceHistory = JsonConvert.DeserializeObject<List<ServiceReservationDto>>(jsonString);
                }

            }

            var listOfNewGetAllReservationDto = new List<GetAllReservationsDto>();
            foreach (var item in listOfServiceHistory)
            {
                listOfNewGetAllReservationDto.Add(new GetAllReservationsDto()
                {
                    Id = item.Id,
                    Date = item.Date,
                    AutoMotivesName = listOfAutoMotives.Where(x => x.Id == item.AutoMotiveRepairId).Select(x => x.Name).FirstOrDefault(),
                    VehiclesRegisterNumber = listOfVehicles.Where(x => x.Id == item.VehicleId).Select(x => x.RegisterNumber).FirstOrDefault()
                });
            }
            return View(listOfNewGetAllReservationDto);
        }

       
        public IActionResult ClearServiceHistory()
        {
            return View();
        }
    }
}
