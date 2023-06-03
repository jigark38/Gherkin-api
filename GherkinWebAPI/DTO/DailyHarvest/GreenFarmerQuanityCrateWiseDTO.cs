using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.DailyHarvest
{
    public class GreenFarmerQuanityCrateWiseDTO
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
        [JsonProperty("lastHarvestStatus")]
        public string LastHarvestStatus { get; set; }
        [JsonProperty("cropGroupCode")]
        public string CropGroupCode { get; set; }
        [JsonProperty("cropNameCode")]
        public string CropNameCode { get; set; }
        [JsonProperty("pSNumber")]
        public string PSNumber { get; set; }
    }
}