using GherkinWebAPI.Models;
using GherkinWebAPI.Models.RMStock;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GherkinWebAPI.Request.RMStock
{
    public class RMStockInsertRequest
    {
        [JsonProperty("RawMaterialStocks")]
        public RMStockDetailsModel RMStocksDetails { get; set; }

        [JsonProperty("RMStockLotDetails")]
        public List<RMStockLotDetailsModel> RMStockLotDetails { get; set; }
    }
}