using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRegister.Domain.Models
{
    public class Token
    {
        [JsonProperty("token")]
        public string AccessToken { get; set; }
    }
}
