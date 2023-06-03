using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.GRPAndroid.ProdProcessBOM
{
    [Table("Production_Process_Material_Details")]
    public class ProductionProcessMaterialDetails
    {
        [Key]
        [JsonProperty("id")]
        [Column("ID")]
        public int ID { get; set; }

        [JsonProperty("bomCode")]
        [Column("BOM_Code")]
        public string BOMCode { get; set; }

        [JsonProperty("rawMaterialGroupCode")]
        [Column("Raw_Material_Group_Code")]
        public string RawMaterialGroupCode { get; set; }

        [JsonProperty("rawMaterialDetailsCode")]
        [Column("Raw_Material_Details_Code")]
        public string RawMaterialDetailsCode { get; set; }

        [JsonProperty("standardQunatity")]
        [Column("Standard_Qunatity")]
        public decimal StandardQunatity { get; set; }

        [JsonProperty("standardUOM")]
        [Column("Standard_UOM")]
        public string StandardUOM { get; set; }
    }
}