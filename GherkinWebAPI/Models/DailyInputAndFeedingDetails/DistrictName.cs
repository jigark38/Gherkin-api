using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.DailyInputAndFeedingDetails
{
    public class DistrictName
    {
        [JsonProperty("districtCode")]
        public int District_Code { get; set; }
        [JsonProperty("districtName")]
        public string District_Name { get; set; }
    }
}