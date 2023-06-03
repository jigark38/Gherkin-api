using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.LoansAndAdvancesDetails
{
    public class LoansAdvancesDTO
    {
        [JsonProperty("loanAdvNo")]
        public string LoanAdvNo { get; set; }
        [JsonProperty("loginEmployeeID")]
        public string LoginEmployeeID { get; set; }
        [JsonProperty("entryDate")]
        public DateTime EntryDate { get; set; }
        [JsonProperty("orgofficeNo")]
        public int OrgofficeNo { get; set; }
        [JsonProperty("employeeID")]
        public string EmployeeID { get; set; }
        [JsonProperty("lAType")]
        public string LAType { get; set; }
        [JsonProperty("lARequistionDate")]
        public DateTime LARequistionDate { get; set; }
        [JsonProperty("lARequistionAmount")]
        [Column("LA_Requistion_Amount")]
        public int LARequistionAmount { get; set; }
        [JsonProperty("lAApprovedDate")]
        public DateTime LAApprovedDate { get; set; }
        [JsonProperty("lAApprovedAmount")]
        public int LAApprovedAmount { get; set; }
        [JsonProperty("lANoOfInstl")]
        public int LANoOfInstl { get; set; }
        [JsonProperty("lACondition")]
        public string LACondition { get; set; }
        [JsonProperty("lAInterestPercentage")]
        public decimal? LAInterestPercentage { get; set; }
        [JsonProperty("lAMonthlyDeduction")]
        public int LAMonthlyDeduction { get; set; }
        [JsonProperty("lAApprovedEmployeeID")]
        public string LAApprovedEmployeeID { get; set; }
        [JsonProperty("laDeductedTillDate")]
        public int? LaDeductedTillDate { get; set; }
        [JsonProperty("laInstallmentsTillPaid")]
        public int? LaInstallmentsTillPaid { get; set; }
    }
}