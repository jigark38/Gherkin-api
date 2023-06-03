using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.DailyInputAndFeedingDetails
{
    public class CountryName
    {
        [JsonProperty("countryCode")]
        public int Country_Code { get; set; }
        [JsonProperty("countryName")]
        public string Country_Name { get; set; }
    }
}