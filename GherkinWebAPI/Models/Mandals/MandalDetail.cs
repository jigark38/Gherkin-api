using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.Mandals
{
    public class MandalDetail
    {
        [JsonProperty("mandalCode")]
        public int MandalCode { get; set; }
        [JsonProperty("mandalName")]
        public string MandalName { get; set; }
        [JsonProperty("districtCode")]
        public int DistrictCode { get; set; }
        [JsonProperty("stateCode")]
        public int StateCode { get; set; }
        [JsonProperty("countryCode")]
        public int CountryCode { get; set; }


    }
}