using GherkinWebAPI.DTO.HarvestStage;
using System.Collections.Generic;

namespace GherkinWebAPI.Response.HarvestStage
{
    public class HarvestStageResponse
    {
        public HarvestStageResponse()
        {
            HarvestStageMaster = new HarvestStageMasterDto();
            HarvestStageDetails = new List<HarvestStageDetailDto>();
        }
        public HarvestStageMasterDto HarvestStageMaster { get; set; }
        public List<HarvestStageDetailDto> HarvestStageDetails { get; set; }

        public string ResponseMessage { get; set; }

    }
}