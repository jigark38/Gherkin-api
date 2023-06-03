using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class CropName
    {
        [JsonProperty("cropNameCode")]
        public string CropNameCode { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}