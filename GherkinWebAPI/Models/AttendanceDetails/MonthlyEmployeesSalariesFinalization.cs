using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace GherkinWebAPI.Models.AttendanceDetails
{
    [Table("Monthly_Employees_Salaries_Finalization")]
    public class MonthlyEmployeesSalariesFinalization
    {
       
        [JsonProperty("loginEmployeeID")]
        public string Login_Employee_ID { get; set; }

        [JsonProperty("salaryApprovalDate")]
        public DateTime Salary_Approval_Date { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [JsonProperty("salaryAttendanceProcessID")]
        public int Salary_Attendance_Process_ID { get; set; }

        [JsonProperty("orgofficeNo")]
        public int Org_office_No { get; set; }

        [JsonProperty("salaryMonthYear")]
        public DateTime Salary_Month_Year { get; set; }

        [JsonProperty("deductionCanteenperday")]
        public int Deduction_Canteen_per_day { get; set; }

        [JsonProperty("otherDeductionsType")]
        public string Other_Deductions_Type { get; set; }

        [JsonProperty("otherDeductionsAmount")]
        public int Other_Deductions_Amount { get; set; }

        [JsonProperty("employeeID")]
        public string Employee_ID { get; set; }

        [JsonProperty("totalAttendedNoofDays")]
        public decimal Total_Attended_No_of_Days { get; set; }

        [JsonProperty("noofDaysConsiderThisMonth")]
        public decimal No_of_Days_Consider_This_Month { get; set; }

        [JsonProperty("noofDaysCarryForward")]
        public decimal No_of_Days_Carry_Forward { get; set; }

        [JsonProperty("loanDeductionAmount")]
        public int Loan_Deduction_Amount { get; set; }

        [JsonProperty("canteenDeductionAmount")]
        public int Canteen_Deduction_Amount { get; set; }

        [JsonProperty("othersDeductionAmount")]
        public int Others_Deduction_Amount { get; set; }

        [JsonProperty("tDSDeductionAmount")]
        public int TDS_Deduction_Amount { get; set; }

        [JsonProperty("employeePFContribution")]
        public decimal Employee_PF_Contribution { get; set; }

        [JsonProperty("employeeESIContribution")]
        public decimal Employee_ESI_Contribution { get; set; }

        [JsonProperty("employeePTContribution")]
        public int Employee_PT_Contribution { get; set; }

        [JsonProperty("netSalaryPayable")]
        public decimal Net_Salary_Payable { get; set; }
    }
}