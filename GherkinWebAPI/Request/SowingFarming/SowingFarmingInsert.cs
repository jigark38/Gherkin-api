using GherkinWebAPI.Entities.SowingFarming;
using Newtonsoft.Json;

namespace GherkinWebAPI.Request.SowingFarming
{
    public class SowingFarmingInsert
    {
        [JsonProperty("sowingFarmingDetails")]
        public SowingFarmingDetails SowingFarmingDetails { get; set; }

        [JsonProperty("farmingStageDetails")]
        public FarmingStageDetails FarmingStageDetails { get; set; }
    }
}