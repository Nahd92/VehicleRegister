using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Client.Helpers;
using VehicleRegister.Domain.DTO.VehicleDTO;
using VehicleRegister.Domain.DTO.VehicleDTO.Request;
using VehicleRegister.Domain.DTO.VehicleDTO.Response;
using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Models.Auth;

namespace VehicleRegister.Client.Controllers
{
    public class VehicleRegisterController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using (var _httpClient = new HttpClient())
            {
                var requestUrl = HttpClientRoutes.VehicleRoute.Vehicles;
                var response = await _httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var vehicles = JsonConvert.DeserializeObject<List<GetAllVehiclesDto>>(jsonString);
                    return View(vehicles);
                }
            }
            return RedirectToAction("Index", "Home");
        }



        [HttpGet]
        public IActionResult CreateVehicle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle(CreateVehicleRequest request)
        {
            using (var client = new HttpClient())
            {           
                var session = SessionHelper.GetObjectFromJson<LoginModel>(HttpContext.Session, "identity");

                if (session is null) RedirectToAction("Login", "Account");


                var createdVehicle = JsonConvert.SerializeObject(request);
                var content = new StringContent(createdVehicle, Encoding.UTF8, "Application/json");
                var requestUrl = HttpClientRoutes.VehicleRoute.CreateVehicle;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.Token);
                var response =  await client.PostAsync(requestUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return View();
            }
        }
  

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Search(GetAllVehiclesDto search)
        {
            if (search.SearchKeyword != null)
            {
                SessionHelper.SetObjectAsJson(HttpContext.Session, "SearchKeyword", search.SearchKeyword);
            }

            var vehicleAfterSearch = await GetVehicleBySearch(HttpContext.Session.GetString("SearchKeyword"));

            if (vehicleAfterSearch != null && vehicleAfterSearch.Count() > 0)
            {
                return View(vehicleAfterSearch);
            }

            return View("NoVehicleFoundAfterSearch", "VehicleRegister");     
        }



        private async Task<IEnumerable<GetAllVehiclesDto>> GetVehicleBySearch(string searchKeyword)
        {
            if (searchKeyword != null && searchKeyword != string.Empty)
            {
                using (var _httpClient = new HttpClient())
                {
                    var searchVehicle = JsonConvert.DeserializeObject(searchKeyword);
                    var requestUrl = HttpClientRoutes.VehicleRoute.VehicleByRegisterName;
                    var response = await _httpClient.GetAsync(requestUrl + searchVehicle);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = response.Content.ReadAsStringAsync().Result;
                        var vehicles = JsonConvert.DeserializeObject<IEnumerable<GetAllVehiclesDto>>(jsonString);
                        return vehicles;
                    }
                }
            }
            return null;
        }
    }
}
