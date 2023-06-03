using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.BatchProduction
{
    public class BatchScheduledOrderDto
    {
        [JsonProperty("productionScheduleNo")]
        public int Production_Schedule_No { get; set; }

        [JsonProperty("productionScheduleDate")]
        public DateTime Production_Schedule_Date { get; set; }
        [JsonProperty("orgOfficeNo")]
        public int OrgOfficeNo { get; set; }

        [JsonProperty("profInvNo")]
        public string Prof_Inv_No { get; set; }

        [JsonProperty("cbCode")]
        public string C_B_Code { get; set; }

        [JsonProperty("cbName")]
        public string C_B_Name { get; set; }

        [JsonProperty("varietyCode")]
        public string VarietyCode { get; set; }

        [JsonProperty("varietyName")]
        public string VarietyName { get; set; }

        [JsonProperty("gradeCode")]
        public string GradeCode { get; set; }
       
        [JsonProperty("gradeFrom")]
        public int GradeFrom { get; set; }
       
        [JsonProperty("gradeTo")]
        public int GradeTo { get; set; }

        [JsonProperty("packUOM")]
        public string Pack_UOM { get; set; }

        [JsonProperty("qtyDrum")]
        public decimal Qty_Drum { get; set; }
        [JsonProperty("psQuantity")]
        public int PS_Quantity { get; set; }
        [JsonProperty("psProductQuantity]")]
        public int PS_Product_Quantity { get; set; }
        [JsonProperty("pSQtyUOM")]
        public long PS_Qty_UOM { get; set; }

        [JsonProperty("mediaProcessCode")]
        public string Media_Process_Code { get; set; }

        [JsonProperty("mediaProcessName")]        
        public string MediaProcessName { get; set; }
        [JsonProperty("mediaProcessDescRemarks")]
        public string Media_Process_Desc_Remarks { get; set; }

    }
}