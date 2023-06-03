using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.DailyInputAndFeedingDetails
{
    public class StateName
    {
        [JsonProperty("stateCode")]
        public int State_Code { get; set; }
        [JsonProperty("stateName")]
        public string State_Name { get; set; }
    }
}