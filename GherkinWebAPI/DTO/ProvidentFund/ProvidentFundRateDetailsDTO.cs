using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.ProvidentFund
{
    public class ProvidentFundRateDetailsDTO
    {
        [JsonProperty("entrydate")]
        public DateTime Entry_Date { get; set; }
        [JsonProperty("enteredEmpId")]
        public string Entered_Emp_ID { get; set; }
        [JsonProperty("pfEffectiveDate")]
        public DateTime PF_Effective_Date { get; set; }
        [Key]
        [JsonProperty("pfpassingNo")]
        public long PF_Passing_No { get; set; }
        [JsonProperty("pfEffectiveFromDate")]
        public DateTime PF_Effective_From_Date { get; set; }
        [JsonProperty("pfEffectiveToDate")]
        public DateTime PF_Effective_To_Date { get; set; }
        [JsonProperty("pfStartingAmount")]
        public int PF_Starting_Amount { get; set; }
        [JsonProperty("pfEmployeeContribution")]
        public decimal PF_Employee_Contribution { get; set; }
        [JsonProperty("pfEmployerEPFContribution")]
        public decimal PF_Employer_EPF_Contribution { get; set; }
        [JsonProperty("pfEmployerEPSContribution")]
        public decimal PF_Employer_EPS_Contribution { get; set; }
        [JsonProperty("pfEPFMaxLimit")]
        public long PF_EPF_Max_Limit { get; set; }
        [JsonProperty("pfEPFAdminCharges")]
        public decimal EPF_Admin_Charges { get; set; }
        [JsonProperty("pfEDLISCharges")]
        public decimal PF_EDLIS_Contribution { get; set; }
        [JsonProperty("pfEDLISAdminCharges")]
        public decimal? PF_EDLIS_Admin_Charges { get; set; }
        [JsonProperty("pfTotalEmployerContribution")]
        public decimal? PF_Total_Employer_Contribution { get; set; }
    }
}