using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Domain.DTO.UserDTO.Request;
using VehicleRegister.Domain.Models;
using Microsoft.AspNetCore.Authentication;

namespace VehicleRegister.Client.Controllers
{
    public class AccountController : Controller
    {

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginRequest());
        }

        public async Task<string> GetToken(LoginRequest request)
        {
            using (var _httpClient = new HttpClient())
            {
                var loginValue = JsonConvert.SerializeObject(request);
                var content = new StringContent(loginValue, Encoding.UTF8, "Application/json");
                var requestUrl = "https://localhost:44345/api/Token";

                var response = await _httpClient.PostAsync(requestUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var jsonString = JsonConvert.DeserializeObject<Token>(result);
                    return jsonString.AccessToken;
                }
            }
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var token = await GetToken(request);

            if (string.IsNullOrEmpty(token))
                return View("LoginErrorView");


            var ui = new LoginModel
            {
                UserName = request.UserName,
                Token = token
            };

            var key = "token";
            var str = JsonConvert.SerializeObject(ui);
            HttpContext.Session.SetString(key, str);


            return View("SuccessLogin");
        }

    }
}
