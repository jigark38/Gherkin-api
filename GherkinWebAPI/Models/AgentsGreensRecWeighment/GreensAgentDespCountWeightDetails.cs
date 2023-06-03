using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.AgentsGreensRecWeighment
{
    public class GreensAgentDespCountWeightDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Agent_Crop_Received_No")]
        [JsonProperty("agentCropReceivedNo")]
        [Key]
        public int AgentCropReceivedNo { get; set; }
        [Column("Crop_Scheme_Code")]
        [JsonProperty("cropSchemeCode")]
        public string CropSchemeCode { get; set; }
        [Column("Greens_Agent_GRN_No")]
        [JsonProperty("greensAgentGRNNo")]
        public int GreensAgentGRNNo { get; set; }
        [Column("Agent_Crop_Received_Crates")]
        [JsonProperty("agentCropReceivedCrates")]
        public int AgentCropReceivedCrates { get; set; }

        [Column("Agent_Crop_Received_Qty")]
        [JsonProperty("agentCropReceivedQty")]
        public decimal? AgentCropReceivedQty { get; set; }
    }
}