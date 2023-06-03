using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Request.GreensAgentSupplierDetails
{
    public class SupplierInformationDetailsRequest
    {
        [JsonProperty("agentOrgID")]
        public int? AgentOrgID { get; set; }
        [JsonProperty("agentCreationDate")]
        public DateTime AgentCreationDate { get; set; }
        [JsonProperty("empCreatedID")]
        public string EmpCreatedID { get; set; }
        [JsonProperty("agentOrganisationName")]
        public string AgentOrganisationName { get; set; }
        [JsonProperty("agentOrganisationLegalStatus")]
        public string AgentOrganisationLegalStatus { get; set; }
        [JsonProperty("agentOrganisationAddress")]
        public string AgentOrganisationAddress { get; set; }
        [JsonProperty("countryCode")]
        public int CountryCode { get; set; }
        [JsonProperty("stateCode")]
        public int StateCode { get; set; }
        [JsonProperty("districtCode")]
        public int DistrictCode { get; set; }
        [JsonProperty("placeCode")]
        public int placeCode { get; set; }
        [JsonProperty("placeName")]
        public string placeName { get; set; }
        [JsonProperty("agentPINCode")]
        public int? AgentPINCode { get; set; }
        [JsonProperty("agentManagementName")]
        public string AgentManagementName { get; set; }
        [JsonProperty("agentManagementDesignation")]
        public string AgentManagementDesignation { get; set; }
        [JsonProperty("agentManagementCN")]
        public Int64? AgentManagementCN { get; set; }
        [JsonProperty("agentManagementMID")]
        public string AgentManagementMID { get; set; }
        [JsonProperty("agentOrganisationOfficeNumber")]
        public Int64? AgentOrganisationOfficeNumber { get; set; }
        [JsonProperty("agentOrganisationActivity")]
        public string AgentOrganisationActivity { get; set; }
        [JsonProperty("agentOrganisationGSTN")]
        public string AgentOrganisationGSTN { get; set; }
        [JsonProperty("agentOrganisationWebsite")]
        public string AgentOrganisationWebsite { get; set; }
        [JsonProperty("agentBankDetailsList")]
        public List<AgentBankDetailsRequest> AgentBankDetailsList { get; set; }
        [JsonProperty("agentOrgDocumentsList")]
        public List<AgentOrgDocumentsRequest> AgentOrgDocumentsList { get; set; }
    }

    public class AgentBankDetailsRequest
    {
        [JsonProperty("agentBankCode")]
        public int? AgentBankCode { get; set; }
        [JsonProperty("agentOrgID")]
        public int AgentOrgID { get; set; }
        [JsonProperty("agentOrganisationBankName")]
        public string AgentOrganisationBankName { get; set; }
        [JsonProperty("agentOrganisationBankBranch")]
        public string AgentOrganisationBankBranch { get; set; }
        [JsonProperty("agentOrganisationBankAccountNo")]
        public int AgentOrganisationBankAccountNo { get; set; }
        [JsonProperty("agentOrganisationBankIFSC")]
        public string AgentOrganisationBankIFSC { get; set; }
        [JsonProperty("preferredBank")]
        public string PreferredBank { get; set; }
    }

    public class AgentOrgDocumentsRequest
    {
        [JsonProperty("agentOrgDocNo")]
        public int AgentOrgDocNo { get; set; }
        [JsonProperty("agentOrgID")]
        public int? AgentOrgID { get; set; }
        [JsonProperty("agentDocumentName")]
        public string AgentDocumentName { get; set; }
        [JsonProperty("agentDocumentDetails")]
        public byte[] AgentDocumentDetails { get; set; }
    }

    public class AgentOrgDetailsRequest
    {
        [JsonProperty("agentOrgID")]
        public int? AgentOrgID { get; set; }
        [JsonProperty("agentOrganisationName")]
        public string AgentOrganisationName { get; set; }
    }
}