using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.AgentsGreensRecWeighment
{
    public class GreensAgentDespCountWeightDetailsDTO
    {
        [JsonProperty("agentCropReceivedNo")]
        public int AgentCropReceivedNo { get; set; }
        [JsonProperty("cropSchemeCode")]
        public string CropSchemeCode { get; set; }
        [JsonProperty("From")]
        public int From { get; set; }
        [JsonProperty("Sign")]
        public string Sign { get; set; }
        [JsonProperty("Count")]
        public decimal Count { get; set; }
        [JsonProperty("greensAgentGRNNo")]
        public int GreensAgentGRNNo { get; set; }
        [JsonProperty("agentCropReceivedCrates")]
        public int AgentCropReceivedCrates { get; set; }

        [JsonProperty("agentCropReceivedQty")]
        public decimal? AgentCropReceivedQty { get; set; }
    }
}