using Newtonsoft.Json;
using System;

namespace GherkinWebAPI.Models.RMStock
{
    public class RMStockBranchDetailsModel
    {
        [JsonProperty("stockDate")]
        public DateTime Stock_Date { get; set; }

        [JsonProperty("areaId")]
        public string Area_ID { get; set; }

        [JsonProperty("areaCode")]
        public int Area_Code { get; set; }
    }
}