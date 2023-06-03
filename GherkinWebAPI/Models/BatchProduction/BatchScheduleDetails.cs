using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.BatchProduction
{
    public class BatchScheduleDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]

        [JsonProperty("batchProductionNo")]
        public long Batch_Production_No { get; set; }

        [JsonProperty("orgofficeNo")]
        public int Org_office_No { get; set; }

        [JsonProperty("employeeID")]
        public int Employee_ID { get; set; }

        [JsonProperty("mediaProcessCode")]
        public string Media_Process_Code { get; set; }

        [JsonProperty("batchProductionProcessIdentity")]
        public string Batch_Production_Process_Identity { get; set; }
        [JsonProperty("batchProductionDate")]
        public DateTime Batch_Production_Date { get; set; }

        [JsonProperty("batchProductionProcesswise")]
        public string Batch_Production_Processwise { get; set; }

        [JsonProperty("batchSizeApprox")]
        public decimal Batch_Size_Approx { get; set; }

        [JsonProperty("pSThroughDetails")]
        public string PS_Through_Details { get; set; }

    }

}