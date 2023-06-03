using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.DailyInputAndFeedingDetails
{
    public class MandalName
    {
        [JsonProperty("mandalCode")]
        public int MandalCode { get; set; }
        [JsonProperty("mandalsName")]
        public string MandalsName { get; set; }
    }
}