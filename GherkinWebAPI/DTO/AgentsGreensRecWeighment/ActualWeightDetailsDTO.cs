using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.AgentsGreensRecWeighment
{
    public class ActualWeightDetailsDTO
    {
        [JsonProperty("actualCountWeightNo")]
        public int ActualCountWeightNo { get; set; }
        [JsonProperty("greensAgentGRNNo")]
        public int GreensAgentGRNNo { get; set; }

        [JsonProperty("cropNameCode")]
        public string CropNameCode { get; set; }
        [JsonProperty("cropName")]
        public string cropName { get; set; }

        [JsonProperty("cropSchemeCode")]
        public string CropSchemeCode { get; set; }
        public int From { get; set; }
        [JsonProperty("Sign")]
        public string Sign { get; set; }
        [JsonProperty("Count")]
        public decimal Count { get; set; }

        [JsonProperty("actualWeightNoofCrates")]
        public int ActualWeightNoofCrates { get; set; }

        [JsonProperty("actualCratesTareWeight")]
        public decimal ActualCratesTareWeight { get; set; }

        [JsonProperty("slNoFrom")]
        public string SlNoFrom { get; set; }

        [JsonProperty("slNoTo")]
        public string SlNoTo { get; set; }

        [JsonProperty("actualGrossWeight")]
        public decimal ActualGrossWeight { get; set; }

        [JsonProperty("actualTareWeight")]
        public decimal ActualTareWeight { get; set; }

        [JsonProperty("actualNetWeight")]
        public decimal ActualNetWeight { get; set; }
    }
}