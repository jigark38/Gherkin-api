using Newtonsoft.Json;
using System;

namespace GherkinWebAPI.DTO.RMStock
{
    public class RMStockBranchQuantityDetailDTO
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("stockDate")]
        public DateTime Stock_Date { get; set; }

        [JsonProperty("stockNo")]
        public string Stock_No { get; set; }

        [JsonProperty("rawMaterialGroupCode")]
        public string Raw_Material_Group_Code { get; set; }

        [JsonProperty("rawMaterialDetailsCode")]
        public string Raw_Material_Details_Code { get; set; }

        [JsonProperty("rawMaterialGroupCodeName")]
        public string Raw_Material_Group_Code_Name { get; set; }

        [JsonProperty("rawMaterialDetailsCodeName")]
        public string Raw_Material_Details_Code_Name { get; set; }

        [JsonProperty("rawMaterialUom")]
        public string Raw_Material_UOM { get; set; }

        [JsonProperty("rmStockQuantity")]
        public decimal RM_Stock_Quantity { get; set; }
    }
}