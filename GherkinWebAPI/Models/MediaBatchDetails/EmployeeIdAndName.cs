using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.MediaBatchDetails
{
    public class EmployeeIdAndName
    {
        [JsonProperty("employeeId")]
        public string EmployeeId { get; set; }
        [JsonProperty("employeeName")]
        public string EmployeeName { get; set; }
    }
}