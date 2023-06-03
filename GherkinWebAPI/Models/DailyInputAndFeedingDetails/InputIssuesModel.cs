using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.DailyInputAndFeedingDetails
{
    public class InputIssuesModel
    {
        [JsonProperty("areaCode")]
        public int AreaCode { get; set; }
        [JsonProperty("mifConsumptionNumber")]
        public int MifConsumptionNumber { get; set; }
        [JsonProperty("inputIssuesGrid")]
        public List<InputIssuesGridModel> InputIssuesGrid { get; set; }
        
    }
}