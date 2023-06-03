using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GherkinWebAPI.Models.ProfessionalTaxRates;
using GherkinWebAPI.Models.ProvidentFund;
using Newtonsoft.Json;
namespace GherkinWebAPI.Models.HumanResource
{

    public class EmployeeAttendanceDetailsWrapper
    {
        [JsonProperty("employeeAttendanceDetails")]
        public List<EmployeeAttendanceDetails> employeeAttendanceDetails { get; set; }

        [JsonProperty("epfDetails")]
        public ProvidentFundRateDetails providentFundRateDetails { get; set; }

        [JsonProperty("esiRate")]
        public ESICRate esiRate { get; set; }

        [JsonProperty("taxMaster")]
        public dynamic TaxMaster { get; set; }


        [JsonProperty("loanDetails")]
        public dynamic LoanDetails { get; set; }

        [JsonProperty("attendanceSummaryDetails")]
        public dynamic AttendanceSummaryDetails { get; set; }

        [JsonProperty("totalCount")]
        public long totalCount { get; set; }
    }
    public class EmployeeAttendanceDetails
    {
        [JsonProperty("employeeIDString")]
        public string EmployeeIDString { get; set; }

        [JsonProperty("employeeName")]
        public string EmployeeName { get; set; }

        [JsonProperty("department")]
        public string Department { get; set; }

        [JsonProperty("division")]
        public string Division { get; set; }

        [JsonProperty("employeeNameDesignation")]
        public string EmployeeNameDesignation { get; set; }

        [JsonProperty("employeeID")]
        public int EmployeeID { get; set; }

        [JsonProperty("leavesBF")]
        public decimal LeavesBF { get; set; }

        [JsonProperty("daysAttnd")]
        public decimal DaysAttnd { get; set; }

        [JsonProperty("daysConsider")]
        public decimal DaysConsider { get; set; }

        [JsonProperty("leavesCF")]
        public decimal LeavesCF { get; set; }

        [JsonProperty("grossSalary")]
        public decimal GrossSalary { get; set; }

        [JsonProperty("pFDed")]
        public decimal PFDed { get; set; }

        [JsonProperty("eSIDed")]
        public decimal ESIDed { get; set; }

        [JsonProperty("pTDed")]
        public decimal PTDed { get; set; }

        [JsonProperty("loans")]
        public decimal Loans { get; set; }

        [JsonProperty("canteen")]
        public decimal Canteen { get; set; }

        [JsonProperty("others")]
        public decimal Others { get; set; }

        [JsonProperty("tDS")]
        public decimal TDS { get; set; }

        [JsonProperty("netPayable")]
        public decimal NetPayable { get; set; }

        [JsonProperty("contactorCode")]
        public string ContactorCode { get; set; }


        [JsonProperty("othours")]
        public decimal Othours { get; set; }


        [JsonProperty("noOfDaysWorked")]
        public int NoOfDaysWorked { get; set; }

        [JsonProperty("noOfDaysAddedAfterCalulation")]
        public decimal NoOfDaysAddedAfterCalulation { get; set; }

        [JsonProperty("duration")]
        public decimal Duration { get; set; }

        [JsonProperty("sundayAdded")]
        public decimal SundayAdded { get; set; }

        [JsonProperty("employeePaymentCategory")]
        public string EmployeePaymentCategory { get; set; }


        [JsonProperty("employeeBasicSalary")]
        public decimal EmployeeBasicSalary { get; set; }

        [JsonProperty("employeeDA")]
        public decimal EmployeeDA { get; set; }

        [JsonProperty("employeeCA")]
        public decimal EmployeeCA { get; set; }

        [JsonProperty("perDaySalary")]
        public decimal PerDaySalary { get; set; }


        [JsonProperty("attendanceDaysCount")]
        public int AttendanceDaysCount { get; set; }


        [JsonProperty("noOfDaysCarryForward")]
        public decimal NoOfDaysCarryForward { get; set; }

        [JsonProperty("employmentStatusAsOn")]
        public string EmploymentStatusAsOn { get; set; }

        [JsonProperty("perHourSalary")]
        public decimal PerHourSalary { get; set; }

        [JsonProperty("monthlySalary")]
        public decimal MonthlySalary { get; set; }

        [JsonProperty("oTPay")]
        public decimal OTPay { get; set; }

        [JsonProperty("selected")]
        public bool Selected { get; set; }
    }
}