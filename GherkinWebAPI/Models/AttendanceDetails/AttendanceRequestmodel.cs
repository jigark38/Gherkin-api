using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.AttendanceDetails
{
    public class AttendanceRequestmodel
    {

        [JsonProperty("orgOfficeNo")]
        public int Org_office_No { get; set; }
        [JsonProperty("attendanceDate")]
        public DateTime Attendance_Date  { get;set;}
        [JsonProperty("entryUpdatedByEmployeeID")]
        public string Entry_Updated_by_Employee_ID { get; set; }
        [JsonProperty("attndUpdatedDate")]
        public DateTime Attnd_Updated_Date { get; set; }
        [JsonProperty("attendances")]
        public List<Attendance> Attendances { get; set; }
    }
}