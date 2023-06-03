using Newtonsoft.Json;

namespace GherkinWebAPI.Models.Harvest_Area
{
    public class HarvestAreaVillageDetail
    {
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("areaId")]
        public string AreaId { get; set; }
        [JsonProperty("countryCode")]
        public int CountryCode { get; set; }
        [JsonProperty("stateCode")]
        public int StateCode { get; set; }
        [JsonProperty("districtCode")]
        public int DistrictCode { get; set; }
        [JsonProperty("mandalCode")]
        public int MandalCode { get; set; }
        [JsonProperty("villageCode")]
        public int VillageCode { get; set; }
        [JsonProperty("villageName")]
        public string VillageName { get; set; }
    }
}