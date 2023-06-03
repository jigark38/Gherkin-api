using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO
{
    public class SummaryReceivingDetailsDTO
    {
        
        //[JsonProperty("harvestGRNNo")]
        //public long HarvestGRNNo { get; set; }
        //[JsonProperty("cropCode")]
        //public string CropNameCode { get; set; }
        [JsonProperty("cropSchemeCode")]
        public string CropSchemeCode { get; set; }
        [JsonProperty("noOfCrates")]
        public int NoofCrates { get; set; }
        [JsonProperty("gradeWiseTotalQuantity")] 
        public decimal GradeWiseTotalQuantity { get; set; }
    }
}