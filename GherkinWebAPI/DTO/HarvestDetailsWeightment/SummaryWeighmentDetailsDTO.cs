using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO
{
    public class SummaryWeighmentDetailsDTO
    {
       
        //[JsonProperty("cropCode")]
        //public string CropNameCode { get; set; }
        [JsonProperty("cropSchemeCode")]
        public string CropSchemeCode { get; set; }
        [JsonProperty("weightNoOfCrates")]
        public int HMWeightNoofCrates { get; set; }
        [JsonProperty("grossWeight")]
        public decimal HMWeightGrossWeight { get; set; }
        [JsonProperty("tareWeight")] 
        public decimal HMWeightTareWeight { get; set; }
        [JsonProperty("netWeight")] 
        public decimal HMWeightNetWeight { get; set; }
        [JsonProperty("cratesTareWeight")]
        public decimal HMCratesTareWeight { get; set; }
    }
}