using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.LoansAndAdvancesDetails
{
    public class LoansAdvancesDetail
    {
        [Key]
        [JsonProperty("loanAdvNo")]
        [Column("Loan_Adv_No")]
        public string LoanAdvNo { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("loginEmployeeID")]
        [Column("Login_Employee_ID")]
        public string LoginEmployeeID { get; set; }
        [JsonProperty("entryDate")]
        [Column("Entry_Date")]
        public DateTime EntryDate { get; set; }
        [JsonProperty("orgofficeNo")]
        [Column("Org_office_No")]
        public int OrgofficeNo { get; set; }
        [JsonProperty("employeeID")]
        [Column("Employee_ID")]
        public string EmployeeID { get; set; }
        [JsonProperty("lAType")]
        [Column("LA_Type")]
        public string LAType { get; set; }
        [JsonProperty("lARequistionDate")]
        [Column("LA_Requistion_Date")]
        public DateTime LARequistionDate { get; set; }
        [JsonProperty("lARequistionAmount")]
        [Column("LA_Requistion_Amount")]
        public int LARequistionAmount { get; set; }
        [JsonProperty("lAApprovedDate")]
        [Column("LA_Approved_Date")]
        public DateTime LAApprovedDate { get; set; }
        [JsonProperty("lAApprovedAmount")]
        [Column("LA_Approved_Amount")]
        public int LAApprovedAmount { get; set; }
        [JsonProperty("lANoOfInstl")]
        [Column("LA_No_of_Instl")]
        public int LANoOfInstl { get; set; }
        [JsonProperty("lACondition")]
        [Column("LA_Condition")]
        public string LACondition { get; set; }
        [JsonProperty("lAInterestPercentage")]
        [Column("LA_Interest_Percentage")]
        public decimal? LAInterestPercentage { get; set; }
        [JsonProperty("lAMonthlyDeduction")]
        [Column("LA_Monthly_Deduction")]
        public int LAMonthlyDeduction { get; set; }
        [JsonProperty("lAApprovedEmployeeID")]
        [Column("LA_Approved_Employee_ID")]
        public string LAApprovedEmployeeID { get; set; }

        [JsonProperty("LaDeductedTillDate")]
        [Column("LA_Deducted_Till_Date")]
        public int? LA_Deducted_Till_Date { get; set; }

        [JsonProperty("LaInstallmentsTillPaid")]
        [Column("LA_Installments_Till_Paid")]
        public int? LA_Installments_Till_Paid { get; set; }

    }

}