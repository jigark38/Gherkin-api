using Newtonsoft.Json;
using System;

namespace GherkinWebAPI.DTO.RMStock
{
    public class RMStockBranchDto
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("stockNo")]
        public string Stock_No { get; set; }

        [JsonProperty("stockDate")]
        public DateTime Stock_Date { get; set; }

        [JsonProperty("areadId")]
        public string Area_ID { get; set; }

        [JsonProperty("areaCode")]
        public int Area_Code { get; set; }
    }
}