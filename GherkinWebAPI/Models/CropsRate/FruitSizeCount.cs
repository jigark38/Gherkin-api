using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class FruitSizeCount
    {
        [JsonProperty("cropSchemeFrom")]
        public int CropSchemeFrom { get; set; }
        [JsonProperty("cropSchemeSign")]
        public string CropSchemeSign { get; set; }
    }
}