using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Response
{
    public class FieldStaffDetailsResponse
    {
        public int FieldStaffID { get; set; }
        [JsonProperty("dateOfEntry")]
        public DateTime DateOfEntry { get; set; }
        [JsonProperty("areaID")]
        public string Area_ID { get; set; }
        [JsonProperty("employeeID")]
        public string Employee_ID { get; set; }
        [JsonProperty("employeeName")]
        public string EmployeeName { get; set; }
        [JsonProperty("effectiveDate")]
        public DateTime EffectiveDate { get; set; }
        [JsonProperty("staffType")]
        public string StaffType { get; set; }
        [JsonProperty("loginUserName")]
        public string LoginUserName { get; set; }
        [JsonProperty("departmentCode")]
        public string DepartmentCode { get; set; }
        [JsonProperty("subDepartmentCode")]
        public string SubDepartmentCode { get; set; }
        [JsonProperty("designationCode")]
        public string DesignationCode { get; set; }
        [JsonProperty("areaCode")]
        public int? AreaCode { get; set; }
    }
}