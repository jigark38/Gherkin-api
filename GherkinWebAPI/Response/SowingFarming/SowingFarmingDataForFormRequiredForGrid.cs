using GherkinWebAPI.DTO.SowingFarming;
using Newtonsoft.Json;

using System.Collections.Generic;

namespace GherkinWebAPI.Response.SowingFarming
{
    public class SowingFarmingDataForFormRequiredForGrid
    {
        public SowingFarmingDataForFormRequiredForGrid()
        {
            HarvestDataForSowingFarrmings = new List<HarvestDataForSowingFarrmingDto>();
            HBOMPracticeForSowingFarmings = new List<HBOMPracticeForSowingFarming>();
        }
        [JsonProperty("harvestDataForSowingFarrmingDto")]
        public List<HarvestDataForSowingFarrmingDto> HarvestDataForSowingFarrmings { get; set; }

        [JsonProperty("hbomPracticeForSowingFarmings")]
        public List<HBOMPracticeForSowingFarming> HBOMPracticeForSowingFarmings { get; set; }
    }
}