using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class HarvestArea
    {
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("areaId")]
        public string AreaId { get; set; }
        [JsonProperty("areaName")]
        public string AreaName { get; set; }
        [JsonProperty("countryCode")]
        public int CountryCode { get; set; }
        [JsonProperty("countryName")]
        public string CountryName { get; set; }
        [JsonProperty("stateCode")]
        public int StateCode { get; set; }
        [JsonProperty("stateName")]
        public string StateName { get; set; }
        [JsonProperty("districtCode")]
        public int DistrictCode { get; set; }
        [JsonProperty("districtName")]
        public string DistrictName { get; set; }
        [JsonProperty("mandalCode")]
        public int MandalCode { get; set; }
        [JsonProperty("mandalName")]
        public string MandalName { get; set; }
        [JsonProperty("villageCode")]
        public int VillageCode { get; set; }
        [JsonProperty("villageName")]
        public string VillageName { get; set; }

    }
}