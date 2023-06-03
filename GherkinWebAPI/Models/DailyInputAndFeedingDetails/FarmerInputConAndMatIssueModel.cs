using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.DailyInputAndFeedingDetails
{
    public class FarmerInputConAndMatIssueModel
    {
        [JsonProperty("farmersInputConsumptionDetail")]
        public FarmersInputConsumptionDetails FarmersInputConsumptionDetail { get; set; }
        [JsonProperty("listFarmersMaterialIssueDetail")]
        public List<FarmersMaterialIssueDetails> ListFarmersMaterialIssueDetail { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}