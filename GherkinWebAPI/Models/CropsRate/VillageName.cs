using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class VillageName
    {
        [JsonProperty("villageCode")]
        public int VillageCode { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("areaId")]
        public string Area_ID { get; set; }
    }
}