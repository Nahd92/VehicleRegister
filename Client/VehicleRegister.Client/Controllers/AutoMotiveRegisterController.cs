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
using VehicleRegister.Domain.DTO.AutoMotiveDTO.Request;
using VehicleRegister.Domain.DTO.AutoMotiveDTO.Response;
using VehicleRegister.Domain.Models.Auth;

namespace VehicleRegister.Client.Controllers
{
    public class AutoMotiveRegisterController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using (var _httpClient = new HttpClient())
            {
                var requestUrl = AutoMotiveRoute.AutoMotives;
                var response = await _httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var autoMotives = JsonConvert.DeserializeObject<List<GetAllAutoMotivesDto>>(jsonString);
                    return View(autoMotives);
                }
            }
            return RedirectToAction("Index", "Home");
        }



        [HttpGet]
        public async Task<IActionResult> AutoMotiveDetails(int id)
        {
            using (var _httpClient = new HttpClient())
            {
                var requestUrl = AutoMotiveRoute.AutoMotive + id;
                var response = await _httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var vehicle = JsonConvert.DeserializeObject<GetAutoMotiveDto>(jsonString);
                    return View(vehicle);
                }
            }
            return RedirectToAction("Index", "Home");
        }




        [HttpGet]
        public async Task<IActionResult> UpdateAutoMotive(int? id)
        {
            using (var _httpClient = new HttpClient())
            {
                var requestUrl = AutoMotiveRoute.AutoMotive + id;
                var response = await _httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var vehicle = JsonConvert.DeserializeObject<UpdateAutoMotiveDto>(jsonString);
                    return View(vehicle);
                }
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> UpdateAutoMotive(UpdateAutoMotiveDto update)
        {
            if (ModelState.IsValid)
            {
                using (var _httpClient = new HttpClient())
                {
                    var session = SessionHelper.GetObjectFromJson<LoginModel>(HttpContext.Session, "identity");

                    if (session is null) RedirectToAction("Login", "Account");


                    var updateVehicle = JsonConvert.SerializeObject(update);
                    var content = new StringContent(updateVehicle, Encoding.UTF8, "Application/json");
                    var requestUrl = AutoMotiveRoute.UpdateAutoMotive;
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





        [HttpGet]
        public IActionResult CreateAutoMotive()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAutoMotive(CreateAutoMotiveDto request)
        {
            if (ModelState.IsValid)
            {
                using (var _httpClient = new HttpClient())
                {
                    var session = SessionHelper.GetObjectFromJson<LoginModel>(HttpContext.Session, "identity");

                    if (session is null) RedirectToAction("Login", "Account");


                    var createdAutoMotive = JsonConvert.SerializeObject(request);
                    var content = new StringContent(createdAutoMotive, Encoding.UTF8, "Application/json");
                    var requestUrl = AutoMotiveRoute.CreateAutoMotive;
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
        public async Task<IActionResult> DeleteAutoMotive(int? id)
        {
            using (var _httpClient = new HttpClient())
            {
                var requestUrl = AutoMotiveRoute.AutoMotive + id;
                var response = await _httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var vehicle = JsonConvert.DeserializeObject<DeleteAutoMotiveDto>(jsonString);
                    return View(vehicle);
                }
            }
            return View();
        }

        [HttpPost, ActionName("DeleteAutoMotive")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var _httpClient = new HttpClient())
            {
                var session = SessionHelper.GetObjectFromJson<LoginModel>(HttpContext.Session, "identity");

                if (session is null) RedirectToAction("Login", "Account");

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.Token);
                var requestUrl = AutoMotiveRoute.DeleteAutoMotive + id;
                var response = await _httpClient.DeleteAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index", "Home");
        }


    }
}
