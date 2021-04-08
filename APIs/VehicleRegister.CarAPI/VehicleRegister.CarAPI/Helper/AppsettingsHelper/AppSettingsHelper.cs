using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace VehicleRegister.VehicleAPI.Helper.AppsettingsHelper
{
    public static class AppSettingsHelper
    {

        public static string SecretKey(this IConfiguration configuration) => configuration.GetValue<string>("SecretKey:Key");
        public static string HostName(this IConfiguration configuration) => configuration.GetValue<string>("Url:HostName");
    }
}
