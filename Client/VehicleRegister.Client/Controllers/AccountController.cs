using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Domain.DTO.UserDTO.Response;
using System.Linq;
using VehicleRegister.Domain.Models.Auth;
using VehicleRegister.Client.Helpers;
using VehicleRegister.Domain.DTO.UserDTO.Request;
using System.Net.Http.Headers;

namespace VehicleRegister.Client.Controllers
{
    public class AccountController : Controller
    {

        [HttpGet]
        public async Task<IActionResult> GetUserInformation(string username)
        {
            using (var _httpClient = new HttpClient())
            {
                var session = SessionHelper.GetObjectFromJson<LoginModel>(HttpContext.Session, "identity");

                if (session is null) RedirectToAction("Login", "Account");

                var requestUrl = IdentityRoute.UserInformation + username;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.Token);
                var response = await _httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var vehicles = JsonConvert.DeserializeObject<GetUserInformationDto>(jsonString);
                    return View(vehicles);
                }
            }
            return RedirectToAction("Index", "Home");
        }



 
        [HttpPost]
        public IActionResult Logout()
        {
            var newIdentity = new LoginModel
            {
                IsLoggedIn = false
            };

            SessionHelper.SetObjectAsJson(HttpContext.Session, "identity", newIdentity);

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserRequest register)
        {
            if (ModelState.IsValid)
            {
                using (var _httpClient = new HttpClient())
                {
                    var registerJson = JsonConvert.SerializeObject(register);
                    var stringContent = new StringContent(registerJson, Encoding.UTF8, "Application/Json");
                    var requestUrl = IdentityRoute.Register;

                    var response = await _httpClient.PostAsync(requestUrl, stringContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var newIdentity = new LoginModel
                        {
                            UserName = register.UserName,
                            Password = register.Password,
                            IsUser = true,
                            IsLoggedIn = true
                        };

                        SessionHelper.SetObjectAsJson(HttpContext.Session, "identity", newIdentity);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            var session = SessionHelper.GetObjectFromJson<LoginModel>(HttpContext.Session, "identity");

            return View(session);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginResponse request)
        {
            if (ModelState.IsValid)
            {
                var response = await GetToken(request);

                if (response == null)
                    return View("LoginErrorView");

                var identity = new LoginModel
                {
                    UserName = request.UserName,
                    Token = response.Token,
                    IsAdmin = response.Roles.Contains("Admin"),
                    IsManager = response.Roles.Contains("Manager"),
                    IsLoggedIn = response.IsLoggedIn
                };

                SessionHelper.SetObjectAsJson(HttpContext.Session, "identity", identity);


                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public async Task<LoginResponse> GetToken(LoginResponse request)
        {
            using (var _httpClient = new HttpClient())
            {
                var loginValue = JsonConvert.SerializeObject(request);
                var content = new StringContent(loginValue, Encoding.UTF8, "Application/json");
                var requestUrl = IdentityRoute.Token;

                var response = await _httpClient.PostAsync(requestUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var jsonString = JsonConvert.DeserializeObject<LoginResponse>(result);
                    return jsonString;
                }
            }
            return null;
        }
    }
}


