using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.HarvestDetails
{
    public class GreenFarmerDetailDto
    {
        [JsonProperty("greensFarmersEntryNo")]
        public int GreensFarmersEntryNo { get; set; }
       
        [JsonProperty("greensProcurementNo")]
        public int GreensProcurementNo { get; set; }
        
        [JsonProperty("farmerCode")]
        public string FarmerCode { get; set; }
        
        [JsonProperty("cropSchemeCode")]
        public string CropSchemeCode { get; set; }
        
        [JsonProperty("countwiseTotalCrates")]
        public int CountwiseTotalCrates { get; set; }
        
        [JsonProperty("countwiseTotalQuantity")]
        public decimal CountwiseTotalQuantity { get; set; }
       
        [JsonProperty("lastHarvestStatus")]
        public string LastHarvestStatus { get; set; }
        [JsonProperty("farmerName")]
        public string FarmerName { get; set; }
        [JsonProperty("cropSchemeInfo")]
        public string CropSchemeInfo { get; set; }
        [JsonProperty("farmerAccountNumber")]
        public string FarmerAccountNumber { get; set; }
        [JsonProperty("cropGroupCode")]
        public string CropGroupCode { get; set; }
        [JsonProperty("cropNameCode")]
        public string CropNameCode { get; set; }
        [JsonProperty("pSNumber")]
        public string PSNumber { get; set; }
    }
}