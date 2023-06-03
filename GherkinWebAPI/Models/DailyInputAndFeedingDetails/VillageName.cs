using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.DailyInputAndFeedingDetails
{
    public class VillageName
    {
        [JsonProperty("villageCode")]
        public int Village_Code { get; set; }
        [JsonProperty("villageName")]
        public string Village_Name { get; set; }
    }
}