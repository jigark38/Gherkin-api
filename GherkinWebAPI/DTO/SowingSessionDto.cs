using Newtonsoft.Json;
using System;

namespace GherkinWebAPI.DTO
{
    public class SowingSessionDto
    {
        [JsonProperty("sessionFrom")]
        public DateTime SessionFrom { get; set; }

        [JsonProperty("sessionTo")]
        public DateTime SessionTo { get; set; }

        [JsonProperty("cropNameCode")]
        public string CropNameCode { get; set; }

        [JsonProperty("psNumber")]
        public string PSNumber { get; set; }

        [JsonProperty("farmerNoOfAcresArea")]
        public decimal Farmers_No_of_Acres_Area { get; set; }

        [JsonProperty("AgricultureDripNonDrip")]
        public string Agriculture_DRIP_NONDRIP { get; set; }

        [JsonProperty("farmersAggrementCode")]
        public string Farmers_Agreement_Code { get; set; }

        [JsonProperty("farmersCode")]
        public string Farmers_Code { get; set; }

    }
}