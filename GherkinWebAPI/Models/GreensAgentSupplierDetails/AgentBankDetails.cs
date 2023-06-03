using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.GreensAgentSupplierDetails
{
    public class AgentBankDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Agent_Bank_Code")]
        [JsonProperty("agentBankCode")]
        public int? AgentBankCode { get; set; }
        [Column("Agent_Org_ID")]
        [JsonProperty("agentOrgID")]
        public int AgentOrgID { get; set; }
        [Column("Agent_Organisation_Bank_Name")]
        [JsonProperty("agentOrganisationBankName")]
        public string AgentOrganisationBankName { get; set; }
        [Column("Agent_Organisation_Bank_Branch")]
        [JsonProperty("agentOrganisationBankBranch")]
        public string AgentOrganisationBankBranch { get; set; }
        [Column("Agent_Organisation_Bank_Account_No")]
        [JsonProperty("agentOrganisationBankAccountNo")]
        public int AgentOrganisationBankAccountNo { get; set; }
        [Column("Agent_Organisation_Bank_IFSC")]
        [JsonProperty("agentOrganisationBankIFSC")]
        public string AgentOrganisationBankIFSC { get; set; }
        [Column("Preferred_Bank")]
        [JsonProperty("preferredBank")]
        public string PreferredBank { get; set; }
    }
}