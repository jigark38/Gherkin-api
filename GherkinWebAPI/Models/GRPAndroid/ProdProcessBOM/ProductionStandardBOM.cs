using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.GRPAndroid.ProdProcessBOM
{
    [Table("Production_Standard_BOM")]
    public class ProductionStandardBOM
    {
        [Key]
        [JsonProperty("bomCode")]
        [Column("BOM_Code")]
        public string BOMCode { get; set; }

        [JsonProperty("productionProcessCode")]
        [Column("Production_Process_Code")]
        public string ProductionProcessCode { get; set; }

        [JsonProperty("mediaProcessCode")]
        [Column("Media_Process_Code")]
        public string MediaProcessCode { get; set; }

        [JsonProperty("effectiveDate")]
        [Column("Effective_Date")]
        public DateTime EffectiveDate { get; set; }

        [JsonProperty("standardProductionQty")]
        [Column("Standard_Production_Qty")]
        public int StandardProductionQty { get; set; }

        [JsonProperty("standardUOM")]
        [Column("Standard_UOM")]
        public string StandardUOM { get; set; }
    }
}