using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRegister.Client.Controllers
{
    public class VehicleServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
