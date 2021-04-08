using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using VehicleRegister.Domain.DTO.VehicleDTO.Response;
using VehicleRegister.Domain.Models.Auth;

namespace VehicleRegister.Client.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(new LoginModel());
        }


       [HttpGet]
       public async Task<IActionResult> Vehicles()
        {
            using (var _httpClient = new HttpClient())
            {
                var str = HttpContext.Session.GetString("token");

                if (str is null)
                {
                   return RedirectToAction("Login", "Account");
                }

                var token = JsonConvert.DeserializeObject<LoginModel>(str);
                var requestUrl = "https://localhost:44345/api/Vehicles";
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
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
  
    }
}
