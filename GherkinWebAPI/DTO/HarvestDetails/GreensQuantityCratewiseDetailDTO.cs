using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.HarvestDetails
{
    public class GreensQuantityCratewiseDetailDTO
    {
        [JsonProperty("greensCratewiseEntryNo")]
        public int GreensCratewiseEntryNo { get; set; }
        
        [JsonProperty("greensProcurementNo")]
        public int GreensProcurementNo { get; set; }
        
        [JsonProperty("farmerCode")]
        public string FarmerCode { get; set; }
        
        [JsonProperty("cropSchemeCode")]
        public string CropSchemeCode { get; set; }
        
        [JsonProperty("noOfCrates")]
        public int NoofCrates { get; set; }
        
        [JsonProperty("eachCrateWt")]
        public decimal EachCrateWt { get; set; }
        
        [JsonProperty("crateNoFrom")]
        public int? CrateNoFrom { get; set; }
        
        [JsonProperty("crateNoTo")]
        public int? CrateNoTo { get; set; }
        
        [JsonProperty("grossWeight")]
        public decimal GrossWeight { get; set; }
       
        [JsonProperty("tareweight")]
        public decimal Tareweight { get; set; }
        
        [JsonProperty("crateswiseNetWeight")]
        public decimal CrateswiseNetWeight { get; set; }
        [JsonProperty("farmerName")]
        public string FarmerName { get; set; }
        [JsonProperty("cropSchemeInfo")]
        public string CropSchemeInfo { get; set; }
        [JsonProperty("farmerAccountNumber")]
        public string FarmerAccountNumber { get; set; }
        [JsonProperty("villageCode")]
        public int VillageCode { get; set; }
        [JsonProperty("villageName")]
        public string VillageName { get; set; }
    }
}