using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO
{
    public class GradeDTO
    {
        [JsonProperty("cropGradeHeader")]
        public string CropSchemeFromSign { get; set; }
        [JsonProperty("cropGradeValue")]
        public string CropGradeData { get; set; }

        [JsonProperty("cropSchemeCode")]
        public string CropSchemeCode { get; set; }
        [JsonProperty("noOfCrates")]
        public int? NoofCrates { get; set; }
        [JsonProperty("farmerWiseTotalQuantity")]
        public decimal FarmerWiseTotalQuantity { get; set; }
    }
}