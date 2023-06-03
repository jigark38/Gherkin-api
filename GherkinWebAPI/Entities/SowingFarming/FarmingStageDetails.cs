using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace GherkinWebAPI.Entities.SowingFarming
{
    public class FarmingStageDetails
    {
        [Key]
        [JsonProperty("harvestFarmingNo")]
        public long Harvest_Farming_No { get; set; }

        [JsonProperty("sowingNo")]
        public long Sowing_No { get; set; }

        [JsonProperty("farmimngReportDate")]
        public DateTime Farming_Report_Date { get; set; }

        [JsonProperty("employeeId")]
        public string Employee_ID { get; set; }

        [JsonProperty("hbomDivisionFor")]
        public string HBOM_Division_For { get; set; }

        [JsonProperty("farmingReportDetails")]
        public string Farming_Report_Details { get; set; }

        [JsonProperty("farmingRemarks")]
        public string Farming_Remarks { get; set; }

        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("hsCropPhaseCode")]
        public string HS_Crop_Phase_Code { get; set; }
    }
}