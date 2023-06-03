using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.ManualAttendence
{
    public class ManualAttendenceDto
    {
        [JsonProperty("manualAttendanceID")]
        public Int64 ManualAttendanceID { get; set; }
        [JsonProperty("manualAttendanceProcessID")]
        public Int64 ManualAttendanceProcessID { get; set; }
        [JsonProperty("manualAttendanceDate")]
        public DateTime ManualAttendanceDate { get; set; }
        [JsonProperty("loginUserName")]
        public string LoginUserName { get; set; }
        [JsonProperty("employeeID")]
        public string EmployeeID { get; set; }
        [JsonProperty("employeeName")]
        public string EmployeeName { get; set; }
        [JsonProperty("designationCode")]
        public string DesignationCode { get; set; }
        [JsonProperty("designation")]
        public string Designation { get; set; }
        [JsonProperty("areaId")]
        public string AreaId { get; set; }
        [JsonProperty("areaName")]
        public string AreaName { get; set; }
        [JsonProperty("manualAttendanceStatus")]
        public string ManualAttendanceStatus { get; set; }
        [JsonProperty("passingManualAttendanceDate")]
        public DateTime PassingManualAttendanceDate { get; set; }

    }

    public class ManualAttendenceData
    {
        [JsonProperty("attendanceData")]
        public List<ManualAttendenceDto> AttendanceData { get; set; }
        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }
    }
}