using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.GreensAgentSupplierDetails
{
    public class SupplierInformationDetails
    {
        public SupplierInformationDetails()
        {
            AgentBankDetailsList = new List<AgentBankDetails>();
            AgentOrgDocumentsList = new List<AgentOrgDocuments>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Agent_Org_ID")]
        [JsonProperty("agentOrgID")]
        public int? AgentOrgID { get; set; }
        [Column("Agent_Creation_Date")]
        [JsonProperty("agentCreationDate")]
        public DateTime AgentCreationDate { get; set; }
        [Column("Emp_Created_ID")]    
        [JsonProperty("empCreatedID")]
        public string EmpCreatedID { get; set; }
        [Column("Agent_Organisation_Name")]
        [JsonProperty("agentOrganisationName")]
        public string AgentOrganisationName { get; set; }
        [Column("Agent_Organisation_Legal_Status")]
        [JsonProperty("agentOrganisationLegalStatus")]
        public string AgentOrganisationLegalStatus { get; set; }
        [Column("Agent_Organisation_Address")]
        [JsonProperty("agentOrganisationAddress")]
        public string AgentOrganisationAddress { get; set; }
        [Column("Country_Code")]
        [JsonProperty("countryCode")]
        public int CountryCode { get; set; }
        [Column("State_Code")]
        [JsonProperty("stateCode")]
        public int StateCode { get; set; }
        [Column("District_Code")]
        [JsonProperty("districtCode")]
        public int DistrictCode { get; set; }
        [Column("Place_Code")]
        [JsonProperty("placeCode")]
        public int placeCode { get; set; }
        [Column("Agent_PIN_Code")]
        [JsonProperty("agentPINCode")]
        public int? AgentPINCode { get; set; }
        [Column("Agent_Management_Name")]
        [JsonProperty("agentManagementName")]
        public string AgentManagementName { get; set; }
        [Column("Agent_Management_Designation")]
        [JsonProperty("agentManagementDesignation")]
        public string AgentManagementDesignation { get; set; }
        [Column("Agent_Management_CN")]
        [JsonProperty("agentManagementCN")]
        public Int64? AgentManagementCN { get; set; }
        [Column("Agent_Management_MID")]
        [JsonProperty("agentManagementMID")]
        public string AgentManagementMID { get; set; }
        [Column("Agent_Organisation_Office_Number")]
        [JsonProperty("agentOrganisationOfficeNumber")]
        public Int64? AgentOrganisationOfficeNumber { get; set; }
        [Column("Agent_Organisation_Activity")]
        [JsonProperty("agentOrganisationActivity")]
        public string AgentOrganisationActivity { get; set; }
        [Column("Agent_Organisation_GSTN")]
        [JsonProperty("agentOrganisationGSTN")]
        public string AgentOrganisationGSTN { get; set; }
        [Column("Agent_Organisation_Website")]
        [JsonProperty("agentOrganisationWebsite")]
        public string AgentOrganisationWebsite { get; set; }
        [JsonProperty("agentBankDetailsList")]
        public List<AgentBankDetails> AgentBankDetailsList { get; set; }
        [JsonProperty("agentOrgDocumentsList")]
        public List<AgentOrgDocuments> AgentOrgDocumentsList { get; set; }
    }
}