using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Entities
{
    public class FieldStaffDTO
    {
        [JsonProperty("effectiveDate")]
        public DateTime Effective_Date { get; set; }
        [JsonProperty("staffType")]
        public string StaffType { get; set; }
        [JsonProperty("employeeID")]
        public string EmployeeID { get; set; }
        [JsonProperty("departmentCode")]
        public string DepartmentCode { get; set; }
        [JsonProperty("subDepartmentCode")]
        public string SubDepartmentCode { get; set; }
        [JsonProperty("designationCode")]
        public string DesignationCode { get; set; }
    }
}