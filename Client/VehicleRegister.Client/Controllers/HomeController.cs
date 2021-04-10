using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using VehicleRegister.Client.Helpers;
using VehicleRegister.Domain.DTO.VehicleDTO.Response;
using VehicleRegister.Domain.Models.Auth;

namespace VehicleRegister.Client.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
           var session = SessionHelper.GetObjectFromJson<LoginModel>(HttpContext.Session, "identity");
            return View(session);
        }
        
    }
}
