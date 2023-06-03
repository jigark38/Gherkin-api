using GherkinWebAPI.Models.AgentsGreensRecWeighment;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.AgentsGreensRecWeighment
{
    public class GreensAgentReceivedDetailsDTO
    {
        public GreensAgentReceivedDetailsDTO()
        {
            GreensAgentDespCountWeightDetails = new List<GreensAgentDespCountWeightDetailsDTO>();
            ActualWeightDetails = new List<ActualWeightDetailsDTO>();
            ActualDetails = new List<ActualDetailsDTO>();
        }
        [JsonProperty("greensAgentGRNNo")]
        public int GreensAgentGRNNo { get; set; }
        [JsonProperty("orgOfficeNo")]
        public int? OrgOfficeNo { get; set; }

        [JsonProperty("orgOfficeName")]
        public string OrgOfficeName { get; set; }
        [JsonProperty("greensAgentGRNDateTime")]
        public DateTime GreensAgentGRNDateTime { get; set; }

        [JsonProperty("agentOrgID")]
        public int? AgentOrgID { get; set; }

        [JsonProperty("agentOrganisationName")]
        public string AgentOrganisationName { get; set; }

        [JsonProperty("place")]
        public string Place { get; set; }

        [JsonProperty("invoiceDCNo")]
        public string InvoiceDCNo { get; set; }

        [JsonProperty("invoiceDCDate")]
        public DateTime InvoiceDCDate { get; set; }

        [JsonProperty("cropGroupCode")]
        public string CropGroupCode { get; set; }

        [JsonProperty("cropGroupName")]
        public string CropGroupName { get; set; }

        [JsonProperty("cropNameCode")]
        public string CropNameCode { get; set; }
        [JsonProperty("cropName")]
        public string cropName { get; set; }

        [JsonProperty("pSNumber")]
        public string PSNumber { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("greensAgentDespQty")]
        public decimal? GreensAgentDespQty { get; set; }

        [JsonProperty("greensAgentDespCrates")]
        public int GreensAgentDespCrates { get; set; }

        [JsonProperty("inwardGatePassNo")]
        public string InwardGatePassNo { get; set; }

        [JsonProperty("isOnGoing")]
        public bool IsOnGoing { get; set; }

        [JsonProperty("totalQuantityReceived")]
        public decimal? TotalQuantityReceived { get; set; }

        [JsonProperty("weightMode")]
        public string WeightMode { get; set; }

        [JsonProperty("employeeID")]
        public string EmployeeID { get; set; }

        [JsonProperty("employeeName")]
        public string EmployeeName { get; set; }

        [JsonProperty("greensAgentDespCountWeightDetails")]
        public List<GreensAgentDespCountWeightDetailsDTO> GreensAgentDespCountWeightDetails { get; set; }
        [JsonProperty("actualWeightDetails")]
        public List<ActualWeightDetailsDTO> ActualWeightDetails { get; set; }

        [JsonProperty("actualDetails")]
        public List<ActualDetailsDTO> ActualDetails { get; set;}
    }
}