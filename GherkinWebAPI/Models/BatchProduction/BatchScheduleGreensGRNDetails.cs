using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.BatchProduction
{
    public class BatchScheduleGreensGRNDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [JsonProperty("bsGreensConsumptionNo")]
        public long BS_Greens_Consumption_No { get; set; }

        [JsonProperty("batchProductionNo")]
        public long Batch_Production_No { get; set; }

        [JsonProperty("harvestGRNNo")]
        public long Harvest_GRN_No { get; set; }

        [JsonProperty("greensGradeQtyNo")]
        public int Greens_Grading_Qty_No { get; set; }

        [JsonProperty("cropNameCode")]
        public string Crop_Name_Code { get; set; }

        [JsonProperty("cropSchemeCode")]
        public string Crop_Scheme_Code { get; set; }
        [JsonProperty("bSGradingQuantity")]
        public decimal BS_Grading_Quantity { get; set; }

    }
}