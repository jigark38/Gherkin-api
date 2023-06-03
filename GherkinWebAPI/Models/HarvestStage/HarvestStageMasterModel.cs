using Newtonsoft.Json;
using System;

namespace GherkinWebAPI.Models.HarvestStage
{
    public class HarvestStageMasterModel
    {

        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("dateOfEntry")]
        public DateTime HS_Entry_Date { get; set; }
        [JsonProperty("enteredBy")]
        public string HS_Entered_Employee_ID_By { get; set; }
        [JsonProperty("cropGroupName")]
        public string Crop_Group_Code { get; set; }
        [JsonProperty("cropName")]
        public string Crop_Name_Code { get; set; }
        [JsonProperty("effectiveDate")]
        public DateTime HS_Effective_Date { get; set; }

        [JsonProperty("hbomDivisionFor")]
        public string HBOMDivisionFor { get; set; }

        [JsonProperty("hsTransactionCode")]
        public string HS_Transaction_Code { get; set; }
    }
}
