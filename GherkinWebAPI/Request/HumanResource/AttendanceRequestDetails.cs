using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Request.HumanResource
{
    public class AttendanceRequestDetails
    {
        [JsonProperty("orgOfficeNo")]
        public int Org_office_No { get; set; }

        [JsonProperty("attendanceDate")]
        public int Attendance_Date { get; set; }

        [JsonProperty("employmentType")]
        public string EmploymentType { get; set; }

        [JsonProperty("itemsPerPage")]
        public int ItemsPerPage { get; set; }

        [JsonProperty("pageNo")]
        public int PageNo { get; set; }

        [JsonProperty("department")]
        public string Department { get; set; }

        [JsonProperty("division")]
        public string Division { get; set; }


    }
}