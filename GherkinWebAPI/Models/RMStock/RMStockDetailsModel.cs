using Newtonsoft.Json;
using System;

namespace GherkinWebAPI.Models
{
    public class RMStockDetailsModel
    {
       
        [JsonProperty("stockDate")]
        public DateTime Stock_Date { get; set; }

        [JsonProperty("areaCode")]
        public string Org_office_No { get; set; }

        [JsonProperty("areaName")]
        public string Nature_office_Details { get; set; }

        [JsonProperty("materialGroup")]
        public string Raw_Material_Group_Code { get; set; }

        [JsonProperty("materialName")]
        public string Raw_Material_Details_Code { get; set; }

        [JsonProperty("uom")]
        public string Raw_Material_UOM { get; set; }

        [JsonProperty("detailedQty")]
        public string RM_Stock_Total_Detailed_Qty { get; set; }

        [JsonProperty("totalQuantity")]
        public decimal Raw_Material_Total_QTY { get; set; }

        [JsonProperty("totalMaterialCost")]
        public decimal Raw_Material_Total_Amount { get; set; }
    }
}