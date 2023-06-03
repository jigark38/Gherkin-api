using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.BatchProduction
{
    public class BatchScheduleDummyProduction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]

        [JsonProperty("bSDummyProductionNo")]
        public long BS_Dummy_Production_No { get; set; }

        [JsonProperty("batchProductionNo")]
        public long Batch_Production_No { get; set; }

        [JsonProperty("fPGroupCode")]
        public string FP_Group_Code { get; set; }

        [JsonProperty("fPVarietyCode")]
        public string FP_Variety_Code { get; set; }

        [JsonProperty("fPGradeCode")]
        public string FP_Grade_Code { get; set; }

        [JsonProperty("packUOM")]
        public string Pack_UOM { get; set; }

        [JsonProperty("qtyDrum")]
        public decimal Qty_Drum { get; set; }

        [JsonProperty("pSQuantity")]
        public int PS_Quantity { get; set; }

        [JsonProperty("pSQtyUOM")]
        public long PS_Qty_UOM { get; set; }
        [JsonProperty("mediaProcessDescRemarks")]
        public string Media_Process_Desc_Remarks { get; set; }


    }
}