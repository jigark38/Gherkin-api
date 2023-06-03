using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.AttendanceDetails
{
    public class AttendanceSalaryDetailsRequest
    {
       public List<AttendanceSalaryDetails> attendanceSalaryDetails { get; set; }
    }

    public class AttendanceSalaryDetails
    {
        public MonthlyEmployeesSalariesFinalization monthlyEmployeesSalariesFinalization { get; set; }
        public MonthlyEmployerContributions monthlyEmployerContributions { get; set; }
        public AttendanceSummaryDetails attendanceSummaryDetails { get; set; }
    }
}