using Newtonsoft.Json;
using System.Collections.Generic;


namespace GherkinWebAPI.Models
{
    public class CountryDetail
    {
        [JsonProperty("countryCode")]
        public int CountryCode { get; set; }
        [JsonProperty("countryName")]
        public string CountryName { get; set; }
        [JsonProperty("states")]
        public List<StateDetail> States { get; set; }
    }

    public class StateDetail
    {
        [JsonProperty("stateCode")]
        public int StateCode { get; set; }
        [JsonProperty("stateName")]
        public string StateName { get; set; }
        [JsonProperty("districts")]
        public List<DistrictDetail> Districts { get; set; }
    }

    public class DistrictDetail
    {
        [JsonProperty("districtCode")]
        public int DistrictCode { get; set; }
        [JsonProperty("districtName")]
        public string DistrictName { get; set; }
        [JsonProperty("mandals")]
        public List<MandalDetail> Mandals { get; set; }
    }

    public class MandalDetail
    {
        [JsonProperty("mandalCode")]
        public int MandalCode { get; set; }
        [JsonProperty("mandalName")]
        public string MandalName { get; set; }
        [JsonProperty("villages")]
        public List<VillageDetail> Villages { get; set; }
    }

    public class VillageDetail
    {
        [JsonProperty("villageCode")]
        public int VillageCode { get; set; }
        [JsonProperty("villageName")]
        public string VillageName { get; set; }
    }
}