using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.AttendanceDetails
{

    [Table("Monthly_Employer_Contributions")]
    public class MonthlyEmployerContributions
    {
        [JsonProperty("salaryAttendanceProcessID")]
        public int Salary_Attendance_Process_ID { get; set; }

        [JsonProperty("employeeID")]
        public string Employee_ID { get; set; }

        [Key]
        [JsonProperty("emprContributionsID")]
        public int Empr_Contributions_ID { get; set; }

        [JsonProperty("employerPFContribution")]
        public decimal Employer_PF_Contribution { get; set; }

        [JsonProperty("employerESIContribution")]
        public decimal Employer_ESI_Contribution { get; set; }

    }
}