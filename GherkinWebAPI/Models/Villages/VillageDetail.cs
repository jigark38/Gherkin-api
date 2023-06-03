using Newtonsoft.Json;

namespace GherkinWebAPI.Models.Villages
{
    public class VillageDetail
    {
        [JsonProperty("villageCode")]
        public int VillageCode { get; set; }
        [JsonProperty("villageName")]
        public string VillageName { get; set; }
        [JsonProperty("mandalCode")]
        public int MandalCode { get; set; }
        [JsonProperty("districtCode")]
        public int DistrictCode { get; set; }
        [JsonProperty("stateCode")]
        public int StateCode { get; set; }
        [JsonProperty("countryCode")]
        public int CountryCode { get; set; }

    }
}