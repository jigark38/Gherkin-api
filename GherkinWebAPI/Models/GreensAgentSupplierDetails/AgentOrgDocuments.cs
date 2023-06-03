using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.GreensAgentSupplierDetails
{
    public class AgentOrgDocuments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Agent_Org_Doc_No")]
        [JsonProperty("agentOrgDocNo")]
        public int AgentOrgDocNo { get; set; }
        [Column("Agent_Org_ID")]
        [JsonProperty("agentOrgID")]
        public int? AgentOrgID { get; set; }
        [Column("Agent_Document_Name")]
        [JsonProperty("agentDocumentName")]
        public string AgentDocumentName { get; set; }
        [Column("Agent_Document_Details")]
        [JsonProperty("agentDocumentDetails")]
        public byte[] AgentDocumentDetails { get; set; }
    }
}