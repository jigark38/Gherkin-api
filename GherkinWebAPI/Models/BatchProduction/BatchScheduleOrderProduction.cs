using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.BatchProduction
{
    public class BatchScheduleOrderProduction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]

        [JsonProperty("bSOrderProduction_No")]
        public long BS_Order_Production_No { get; set; }

        [JsonProperty("batchProductionNo")]
        public long Batch_Production_No { get; set; }

        [JsonProperty("pSSalesOrderSchedule_No")]
        public long PS_Sales_Order_Schedule_No { get; set; }

        [JsonProperty("pSDirectOrderScheduleNo")]
        public long PS_Direct_Order_Schedule_No { get; set; }

        [JsonProperty("fPGroupCode")]
        public string FP_Group_Code { get; set; }

        [JsonProperty("fPVarietyCode")]
        public string FP_Variety_Code { get; set; }

        [JsonProperty("fPGradeCode")]
        public string FP_Grade_Code { get; set; }

        [JsonProperty("packUOM")]
        public string Pack_UOM { get; set; }

        [JsonProperty("bSProductionQtyNos")]
        public int BS_Production_Qty_Nos { get; set; }

        [JsonProperty("bSProductionQtyinUOM")]
        public long BS_Production_Qty_in_UOM { get; set; }


    }
}