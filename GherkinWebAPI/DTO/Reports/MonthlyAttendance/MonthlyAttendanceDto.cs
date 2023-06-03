using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.Reports.MonthlyAttendance
{
    public class MonthlyAttendanceDto
    {

        [JsonProperty("AttndUpdatedNo")]
        public int AttndUpdatedNo { get; set; }

        [JsonProperty("OrgOfficeNo")]
        public int OrgOfficeNo { get; set; }

        [JsonProperty("AttendanceDate")]
        public DateTime AttendanceDate { get; set; }

        [JsonProperty("EntryUpdatedbyEmployeeID")]
        public string EntryUpdatedbyEmployeeID { get; set; }

        [JsonProperty("AttndUpdatedDate")]
        public DateTime AttndUpdatedDate { get; set; }

        [JsonProperty("EmployeeID")]
        public string EmployeeID { get; set; }

        [JsonProperty("EmpBiometricID")]
        public int EmpBiometricID { get; set; }

        [JsonProperty("EmployeeName")]
        public string EmployeeName { get; set; }

        [JsonProperty("DepartmentCode")]
        public string DepartmentCode { get; set; }

        [JsonProperty("SubDepartmentCode")]
        public string SubDepartmentCode { get; set; }

        [JsonProperty("EmployeeDivision")]
        public string EmployeeDivision { get; set; }

        [JsonProperty("EmploymentStatusAsOn")]
        public string EmploymentStatusAsOn { get; set; }

        [JsonProperty("AttndInTime_Updated")]
        public DateTime AttndInTime_Updated { get; set; }

        [JsonProperty("AttndInOutUpdated")]
        public DateTime AttndInOutUpdated { get; set; }

        [JsonProperty("Duration")]
        public decimal Duration { get; set; }

        [JsonProperty("OThours")]
        public decimal OThours { get; set; }
    }
}