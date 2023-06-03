using Newtonsoft.Json;
using System;

namespace GherkinWebAPI.Models.AttendanceDetails
{
    public class Attendance
    {
        [JsonProperty("indRegId")]
        public int IndRegId { get; set; }
        [JsonProperty("employeeName")]
        public string EmployeeName { get; set; }
        [JsonProperty("department")]
        public string Department { get; set; }
        [JsonProperty("subDepartment")]
        public string SubDepartment { get; set; }
        [JsonProperty("departmentCode")]
        public string DepartmentCode { get; set; }
        [JsonProperty("subDepartmentCode")]
        public string SubDepartmentCode { get; set; }
        [JsonProperty("designation")]
        public string Designation { get; set; }
        [JsonProperty("dateTimeRecord")]
        public DateTime? DateTimeRecord { get; set; }
        //[JsonProperty("dateOnlyRecord")]
        //public string DateOnlyRecord { get; set; }
        [JsonProperty("time")]
        public string Time { get; set; }
        [JsonProperty("inTime")]
        public string InTime { get; set; }
        [JsonProperty("outTime")]
        public string OutTime { get; set; }
        [JsonProperty("duration")]
        public string Duration { get; set; }
        [JsonProperty("dwInOutMode")]
        public int? DwInOutMode { get; set; }
        [JsonProperty("overtime")]
        public string overtime { get; set; }
        [JsonProperty("divison")]
        public string Division { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }
        
        [JsonProperty("employeeId")]
        public string EmployeeId { get; set; }

        [JsonProperty("shiftDuration")]
        public TimeSpan ShiftDuration { get; set; }

        [JsonProperty("isChecked")]
        public bool IsChecked { get; set; }
    }
}