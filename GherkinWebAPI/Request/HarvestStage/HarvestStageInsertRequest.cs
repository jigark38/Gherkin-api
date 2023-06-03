using GherkinWebAPI.Models.HarvestStage;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GherkinWebAPI.Request.HarvestStage
{
    public class HarvestStageInsertRequest
    {
        [JsonProperty("harvestStageMaster")]
        public HarvestStageMasterModel HarvestStageMaster { get; set; }
        [JsonProperty("harvestStageDetails")]
        public List<HarvestStageDetailsModel> HarvestStageDetails { get; set; }

    }
}