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
                var requestUrl = VehicleRoute.Vehicles;
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
            if (ModelState.IsValid)
            {
                using (var _httpClient = new HttpClient())
                {
                    var session = SessionHelper.GetObjectFromJson<LoginModel>(HttpContext.Session, "identity");

                    if (session is null) return RedirectToAction("Login", "Account");


                    var createdVehicle = JsonConvert.SerializeObject(request);
                    var content = new StringContent(createdVehicle, Encoding.UTF8, "Application/json");
                    var requestUrl = VehicleRoute.CreateVehicle;
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.Token);
                    var response = await _httpClient.PostAsync(requestUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }
  
        [HttpGet]
        public async Task<IActionResult> DeleteVehicle(int? id)
        {
            using (var _httpClient = new HttpClient())
            {
                var requestUrl = VehicleRoute.GetVehicle + id;
                var response = await _httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var vehicle = JsonConvert.DeserializeObject<DeleteVehicleDto>(jsonString);
                    return View(vehicle);
                }
            }
            return View();
        }

        [HttpPost, ActionName("DeleteVehicle")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var _httpClient = new HttpClient())
            {
                var session = SessionHelper.GetObjectFromJson<LoginModel>(HttpContext.Session, "identity");

                if (session is null) return RedirectToAction("Login", "Account");

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.Token);             
                var requestUrl = VehicleRoute.DeleteVehicel + id;
                var response = await _httpClient.DeleteAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> VehicleDetails(int id)
        {
            using (var _httpClient = new HttpClient())
            {
                var requestUrl = VehicleRoute.GetVehicle + id;
                var response = await _httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var vehicle = JsonConvert.DeserializeObject<GetVehicleDto>(jsonString);
                    return View(vehicle);
                }
            }
            return RedirectToAction("Index", "Home");
        }





        [HttpGet]
        public async Task<IActionResult> VehicleUpdate(int? id)
        {
            using (var _httpClient = new HttpClient())
            {                    
                var requestUrl = VehicleRoute.GetVehicle + id;
                var response = await _httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var vehicle = JsonConvert.DeserializeObject<UpdateVehicleRequest>(jsonString);
                    return View(vehicle);
                }
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> VehicleUpdate(UpdateVehicleRequest update)
        {
            if (ModelState.IsValid)
            {
                using (var _httpClient = new HttpClient())
                {
                    var session = SessionHelper.GetObjectFromJson<LoginModel>(HttpContext.Session, "identity");

                    if (session is null) return RedirectToAction("Login", "Account");


                    var updateVehicle = JsonConvert.SerializeObject(update);
                    var content = new StringContent(updateVehicle, Encoding.UTF8, "Application/json");
                    var requestUrl = VehicleRoute.UpdateVehicle;
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.Token);
                    var response = await _httpClient.PutAsync(requestUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
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
                    var requestUrl = VehicleRoute.VehicleByRegisterName;
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
