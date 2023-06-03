using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.AgentsGreensRecWeighment
{
    public class GreensAgentGradesActualDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Agent_Received_No")]
        [JsonProperty("agentReceivedNo")]
        [Key]
        public int AgentReceivedNo { get; set; }

        [Column("Greens_Agent_GRN_No")]
        [JsonProperty("greensAgentGRNNo")]
        public int GreensAgentGRNNo { get; set; }

        [Column("Crop_Name_Code")]
        [JsonProperty("cropNameCode")]
        public string CropNameCode { get; set; }

        [Column("Crop_Scheme_Code")]
        [JsonProperty("cropSchemeCode")]
        public string CropSchemeCode { get; set; }

        [Column("Count_Total_Crates")]
        [JsonProperty("countTotalCrates")]
        public int CountTotalCrates { get; set; }

        [Column("Count_Total_Weight")]
        [JsonProperty("countTotalWeight")]
        public decimal CountTotalWeight { get; set; }
    }
}