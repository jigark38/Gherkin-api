using GherkinWebAPI.Models.RMStock;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GherkinWebAPI.Request.RMStock
{
    public class RMStockBranchInsertRequest
    {
        [JsonProperty("rmStockBranchDetailsModel")]
        public RMStockBranchDetailsModel RMStockBranchDetailsModel { get; set; }

        [JsonProperty("rmStockBranchQuantityModel")]
        public List<RMStockBranchQuantityModel> RMStockBranchQuantityModel { get; set; }
    }
}