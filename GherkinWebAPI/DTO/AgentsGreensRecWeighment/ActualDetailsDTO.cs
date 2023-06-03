using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.AgentsGreensRecWeighment
{
    public class ActualDetailsDTO
    {
        [JsonProperty("agentReceivedNo")]
        public int AgentReceivedNo { get; set; }

        [JsonProperty("greensAgentGRNNo")]
        public int GreensAgentGRNNo { get; set; }

        [JsonProperty("cropNameCode")]
        public string CropNameCode { get; set; }
        [JsonProperty("cropName")]
        public string cropName { get; set; }

        [JsonProperty("cropSchemeCode")]
        public string CropSchemeCode { get; set; }
        [JsonProperty("From")]
        public int From { get; set; }
        [JsonProperty("Sign")]
        public string Sign { get; set; }
        [JsonProperty("Count")]
        public decimal Count { get; set; }

        [JsonProperty("countTotalCrates")]
        public int CountTotalCrates { get; set; }

        [JsonProperty("countTotalWeight")]
        public decimal CountTotalWeight { get; set; }
    }
}