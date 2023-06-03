using Newtonsoft.Json;
using System;

namespace GherkinWebAPI.Models.RMStock
{
    public class RMStockBranchQuantityModel
    {
        [JsonProperty("rawMaterialGroupCode")]
        public string Raw_Material_Group_Code { get; set; }

        [JsonProperty("rawMaterialDetailsCode")]
        public string Raw_Material_Details_Code { get; set; }

        [JsonProperty("rawMaterialUom")]
        public string Raw_Material_UOM { get; set; }

        [JsonProperty("rmStockQuantity")]
        public decimal RM_Stock_Quantity { get; set; }
    }
}