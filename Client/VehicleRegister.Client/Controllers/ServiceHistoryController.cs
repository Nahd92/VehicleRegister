using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

namespace VehicleRegister.Client.Controllers
{
    public class ServiceHistoryController : Controller
    {
        private readonly IServiceHistory _serviceHistory;
        public ServiceHistoryController(IServiceHistory serviceHistory)
        {
            _serviceHistory = serviceHistory;
        }




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

                listOfVehicles = await _serviceHistory.GetAllVehicles(listOfVehicles, _httpClient);
                listOfAutoMotives = await _serviceHistory.GetAllAutoMotives(listOfAutoMotives, _httpClient);

                serviceList = await _serviceHistory.GetAllServices(serviceList, _httpClient, session);

                historyList = await _serviceHistory.GetAllHistory(historyList, _httpClient, session);
            }

            var listOfNewGetAllReservationDto = _serviceHistory.AddAllListToOne(listOfVehicles, listOfAutoMotives, serviceList, historyList);

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
                var requestUrl = ServiceHistorys.DeleteReservations;
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
                var requestUrl = ServiceHistorys.DeleteReservation + id;
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
