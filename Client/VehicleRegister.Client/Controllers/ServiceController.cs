using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Client.Helpers;
using VehicleRegister.Domain.DTO.AutoMotiveDTO.Response;
using VehicleRegister.Domain.DTO.ReservationsDTO.Request;
using VehicleRegister.Domain.DTO.VehicleDTO.Response;
using VehicleRegister.Domain.Models.Auth;

namespace VehicleRegister.Client.Controllers
{
    public class ServiceController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> BookService()
        {
           
                var listOfVehicles = new List<GetAllVehiclesDto>();
                var listOfAutoMotives = new List<GetAllAutoMotivesDto>();

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
                }

                var vehList = listOfVehicles.Select(c => new SelectListItem()
                {
                    Text = c.RegisterNumber,
                    Value = c.Id.ToString()
                });

                var autoList = listOfAutoMotives.Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });

                ViewBag.Vehicles = vehList;
                ViewBag.AutoMotives = autoList;

                return View();
        }

        [HttpPost]
        public async Task<IActionResult> BookService(ClientCreateReservationDto request)
        {
            if (ModelState.IsValid)
            {
                using (var _httpClient = new HttpClient())
                {
                    var session = SessionHelper.GetObjectFromJson<LoginModel>(HttpContext.Session, "identity");

                    if (session is null) return RedirectToAction("Login", "Account");


                    var createdReservation = JsonConvert.SerializeObject(request);
                    var content = new StringContent(createdReservation, Encoding.UTF8, "Application/json");
                    var requestUrl = ServiceRoute.CreateBooking;
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.Token);
                    var response = await _httpClient.PostAsync(requestUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "ServiceHistory");
                    }
                }
            }
            return RedirectToAction("BookService");
        }
    }
}
