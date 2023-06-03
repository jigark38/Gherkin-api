using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO
{
    public class CropRateDTO
    {
        [JsonProperty("cropRateNumber")]
        public string Crop_Rate_No { get; set; }
        [JsonProperty("cropRateEntryDate")]
        public DateTime CropRateEntryDate { get; set; }
        [JsonProperty("cropRateEnteredByEmpId")]
        public string CropRateEnteredByEmpId { get; set; }
        [JsonProperty("cropGroupCode")]
        public string CropGroupCode { get; set; }
        [JsonProperty("cropGroupName")]
        public string CropGroupName { get; set; }
        [JsonProperty("allAreas")]
        public string AllAreas { get; set; }
        [JsonProperty("areaId")]
        public string AreaId { get; set; }
        [JsonProperty("allVillages")]
        public string AllVillages { get; set; }
        [JsonProperty("villageCode")]
        public string VillageCode { get; set; }
        [JsonProperty("cropSchemeCode")]
        public string CropSchemeCode { get; set; }
        [JsonProperty("cropRateEffectiveDate")]
        public DateTime CropRateEffectiveDate { get; set; }
        [JsonProperty("cropCount")]
        public decimal CropCount { get; set; }
        [JsonProperty("cropSchemeFrom")]
        public int CropSchemeFrom { get; set; }
        [JsonProperty("cropSchemeSign")]
        public string CropSchemeSign { get; set; }
        [JsonProperty("cropRateAsperAssociation")]
        public decimal CropRateAsperAssociation { get; set; }
        [JsonProperty("cropRatePerUOM")]
        public string CropRatePerUOM { get; set; }
        [JsonProperty("message")]
        public string message { get; set; }

        [JsonProperty("psNumber")]
        public string PsNumber { get; set; }
    }
}