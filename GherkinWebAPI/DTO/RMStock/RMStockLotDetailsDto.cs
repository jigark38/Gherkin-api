using Newtonsoft.Json;
using System;

namespace GherkinWebAPI.DTO.RMStock
{
    public class RMStockLotDetailsDto
    {
        [JsonProperty("grnId")]
        public int RM_stock_Lot_Details_ID { get; set; }

        [JsonProperty("stockNo")]
        public string Stock_No { get; set; }

        [JsonProperty("grnDate")]
        public DateTime RM_Stock_LOT_GRN_Date { get; set; }

        [JsonProperty("grnNo")]
        public string RM_Stock_LOT_GRN_No { get; set; }

        [JsonProperty("quantity")]
        public decimal RM_Stock_Lot_Grn_Qty { get; set; }

        [JsonProperty("rate")]
        public decimal RM_Stock_Lot_Grn_Rate { get; set; }

        [JsonProperty("amount")]
        public decimal RM_Stock_Lot_Grn_Amount { get; set; }
    }
}