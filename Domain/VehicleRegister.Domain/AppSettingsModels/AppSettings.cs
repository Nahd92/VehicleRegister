using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRegister.Domain.AppSettingsModels
{
    public static class AppSettings
    {
        public static string HostName { get; set; }
        public static string SecretKey { get; set; }
        public static string AdminPassword { get; set; }
        public static string ManagerPassword { get; set; }
    }
}
