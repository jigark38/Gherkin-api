using Newtonsoft.Json;
using System;


namespace GherkinWebAPI.DTO
{
    public class ProductVarietyDto
    {
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("groupCode")]
        public string GroupCode { get; set; }
        [JsonProperty("varietyCode")]
        public string VarietyCode { get; set; }
        [JsonProperty("varietyName")]
        public string VarietyName { get; set; }
        [JsonProperty("scientificName")]

        public string ScientificName { get; set; }
    }
}