using Newtonsoft.Json;


namespace GherkinWebAPI.Models.Districts
{
    public class DistrictDetail
    {
        [JsonProperty("districtCode")]
        public int DistrictCode { get; set; }
        [JsonProperty("districtName")]
        public string DistrictName { get; set; }
        [JsonProperty("stateCode")]
        public int StateCode { get; set; }
        [JsonProperty("countryCode")]
        public int CountryCode { get; set; }

    }
}