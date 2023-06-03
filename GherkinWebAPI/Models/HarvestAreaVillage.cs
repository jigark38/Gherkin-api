using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class HarvestAreaVillage
    {
        [Key]
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("areaId")]
        public string Area_ID { get; set; }
        [JsonProperty("countryCode")]
        public int Country_Code { get; set; }
        [JsonProperty("stateCode")]
        public int State_Code { get; set; }
        [JsonProperty("districtCode")]
        public int District_Code { get; set; }
        [JsonProperty("mandalCode")]
        public int Mandal_Code { get; set; }
        [JsonProperty("villageCode")]
        public int Village_Code { get; set; }
        //public string Mandal_Code { get; set; }
    }
}