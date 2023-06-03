using Newtonsoft.Json;

namespace GherkinWebAPI.Models.HarvestStage
{
    public class HarvestStageDetailsModel
    {
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("cropPhaseName")]
        public string HS_Crop_Phase_Name { get; set; }
        [JsonProperty("daysAfterSowingFrom")]
        public int HS_Days_After_Sowing_From { get; set; }
        [JsonProperty("daysTo")]
        public int HS_Days_After_Sowing_To { get; set; }
        [JsonProperty("harvestDetails")]
        public string HS_Harvest_Details { get; set; }
        [JsonProperty("hsTransactionCode")]
        public string HS_Transaction_Code { get; set; }
    }
}