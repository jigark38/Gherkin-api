using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.AgentsGreensRecWeighment
{
    public class GreensAgentReceivedDetails
    {
        public GreensAgentReceivedDetails()
        {
            GreensAgentDespCountWeightDetails = new List<GreensAgentDespCountWeightDetails>();
            GreensAgentActualWeightDetails = new List<GreensAgentActualWeightDetails>();
            GreensAgentGradesActualDetails = new List<GreensAgentGradesActualDetails>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Greens_Agent_GRN_No")]
        [JsonProperty("greensAgentGRNNo")]
        [Key]
        public int GreensAgentGRNNo { get; set; }
        [Column("Org_office_No")]
        [JsonProperty("orgOfficeNo")]
        public int? OrgOfficeNo { get; set; }

        [Column("Greens_Agent_GRN_Date_Time")]
        [JsonProperty("greensAgentGRNDateTime")]
        public DateTime GreensAgentGRNDateTime { get; set; }

        [Column("Agent_Org_ID")]
        [JsonProperty("agentOrgID")]
        public int? AgentOrgID { get; set; }

        [Column("Invoice_DC_No")]
        [JsonProperty("invoiceDCNo")]
        public string InvoiceDCNo { get; set; }

        [Column("Invoice_DC_Date")]
        [JsonProperty("invoiceDCDate")]
        public DateTime InvoiceDCDate { get; set; }

        [Column("Crop_Group_Code")]
        [JsonProperty("cropGroupCode")]
        public string CropGroupCode { get; set; }

        [Column("Crop_Name_Code")]
        [JsonProperty("cropNameCode")]
        public string CropNameCode { get; set; }

        [Column("PS_Number")]
        [JsonProperty("pSNumber")]
        public string PSNumber { get; set; }

        [Column("Greens_Agent_Desp_Qty")]
        [JsonProperty("greensAgentDespQty")]        
        public decimal? GreensAgentDespQty { get; set; }

        [Column("Greens_Agent_Desp_Crates")]
        [JsonProperty("greensAgentDespCrates")]
        public int GreensAgentDespCrates { get; set; }

        [Column("Inward_Gate_Pass_No")]
        [JsonProperty("inwardGatePassNo")]
        public string InwardGatePassNo { get; set; }

        [Column("IsOnGoing")]
        [JsonProperty("isOnGoing")]
        public bool IsOnGoing { get; set; }

        [Column("Total_Quantity_Received")]
        [JsonProperty("totalQuantityReceived")]
        public decimal? TotalQuantityReceived { get; set; }

        [Column("Weight_Mode")]
        [JsonProperty("weightMode")]
        public string WeightMode { get; set; }

        [Column("Employee_ID")]
        [JsonProperty("employeeID")]
        public string EmployeeID { get; set; }

        [JsonProperty("greensAgentDespCountWeightDetails")]
        public List<GreensAgentDespCountWeightDetails> GreensAgentDespCountWeightDetails { get; set; }
        [JsonProperty("greensAgentActualWeightDetails")]
        public List<GreensAgentActualWeightDetails> GreensAgentActualWeightDetails { get; set; }

        [JsonProperty("greensAgentGradesActualDetails")]
        public List<GreensAgentGradesActualDetails> GreensAgentGradesActualDetails { get; set; }
    }
}