using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRegister.Domain.AppSettingsModels
{
    public class AppSettings
    {
        public string HostName { get; set; }
        public string SecretKey { get; set; }
    }
}
