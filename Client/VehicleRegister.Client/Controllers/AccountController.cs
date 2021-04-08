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
using Microsoft.AspNetCore.Identity;
using VehicleRegister.Domain.DTO.UserDTO.Response;
using System.Linq;
using VehicleRegister.Domain.Models.Auth;

namespace VehicleRegister.Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        public async Task<LoginResponse> GetToken(LoginResponse request)
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
                    var jsonString = JsonConvert.DeserializeObject<LoginResponse>(result);


            
                    return jsonString;
                }
            }
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginResponse request)
        {
            if (ModelState.IsValid)
            {
                var response = await GetToken(request);

                if (string.IsNullOrEmpty(response.Token))
                    return View("LoginErrorView");


                var result = await signInManager.PasswordSignInAsync(request.UserName, request.Password, request.RememberMe, false);

                var id = new LoginModel
                {
                    UserName = request.UserName,
                    Token = response.Token,
                    IsAdmin = response.Roles.Contains("Admin"),
                    IsManager = response.Roles.Contains("Manager"),
                    IsLoggedIn = response.IsLoggedIn
                };

                var key = "token";
                var str = JsonConvert.SerializeObject(id);
                HttpContext.Session.SetString(key, str);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Login", "Account");
        }
    }
}
