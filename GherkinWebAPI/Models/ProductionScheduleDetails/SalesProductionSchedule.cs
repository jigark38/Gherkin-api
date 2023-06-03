using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class SalesProductionSchedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public long PS_Sales_Order_Schedule_No { get; set; }

        [JsonProperty("productionScheduleNo")]
        public long Production_Schedule_No { get; set; }

        [JsonProperty("profInvNo")]
        public string Prof_Inv_No { get; set; }

        [JsonProperty("fpGroupCode")]
        public string FP_Group_Code { get; set; }

        [JsonProperty("fpVarietyCode")]
        public string FP_Variety_Code { get; set; }

        [JsonProperty("fpGradeCode")]
        public string FP_Grade_Code { get; set; }

        [JsonProperty("packUOM")]
        public string Pack_UOM { get; set; }

        [JsonProperty("qtyDrum")]
        public decimal Qty_Drum { get; set; }

        [JsonProperty("orderQuantity")]
        public int Order_Quantity { get; set; }

        [JsonProperty("psQuantity")]
        public int PS_Quantity { get; set; }

        [JsonProperty("psProductQuantity]")]
        public int PS_Product_Quantity { get; set; }

        [JsonProperty("mediaProcessCode")]
        public string Media_Process_Code { get; set; }

        [JsonProperty("mediaProcessDescRemarks")]
        public string Media_Process_Desc_Remarks { get; set; }

        [JsonProperty("deliverBy")]
        public DateTime? delivery_by { get; set; }

    }
}