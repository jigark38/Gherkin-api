using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.SowingFarming
{
    public class HarvestDataForSowingFarrmingDto
    {
        [JsonProperty("hsTransactionCode")]
        public string HS_Transaction_Code { get; set; }

        [JsonProperty("hsEffectiveDate")]
        public DateTime HS_Effective_Date { get; set; }

        [JsonProperty("hsCropPhaseName")]
        public string HS_Crop_Phase_Name { get; set; }

        [JsonProperty("hsCropPhaseCode")]
        public string HS_Crop_Phase_Code { get; set; }

        [JsonProperty("hsDayAfterSowingFrom")]
        public int HS_Days_After_Sowing_From { get; set; }

        [JsonProperty("hsDayAfterSowingTo")]
        public int HS_Days_After_Sowing_To { get; set; }
    }
}