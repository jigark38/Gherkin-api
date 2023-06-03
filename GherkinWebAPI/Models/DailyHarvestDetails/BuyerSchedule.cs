using Newtonsoft.Json;
using System;

namespace GherkinWebAPI.Models.DailyHarvestDetails
{
    public class BuyerSchedule
    {
        [JsonProperty("buyerName")]
        public string BuyerName { get; set; }
        [JsonProperty("buyerId")]
        public string BuyerId { get; set; }
        [JsonProperty("area")]
        public string Area { get; set; }
        [JsonProperty("areaId")]
        public string AreaId { get; set; }
        [JsonProperty("despDate")]
        public DateTime DespDate { get; set; }
        [JsonProperty("despNo")]
        public int DespNo { get; set; }
        [JsonProperty("greensProcurementNo")]
        public int? GreensProcurementNo { get; set; }
    }
}