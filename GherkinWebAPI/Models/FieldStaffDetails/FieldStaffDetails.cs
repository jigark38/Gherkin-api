using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class FieldStaffDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("FS_ID")]
        [JsonProperty("fieldStaffID")]
        public int FieldStaffID { get; set; }
        [Column("FS_Entry_Date")]
        [JsonProperty("dateOfEntry")]
        public DateTime DateOfEntry { get; set; }
        [JsonProperty("areaID")]
        public string Area_ID { get; set; }
        [JsonProperty("employeeID")]
        public string Employee_ID { get; set; }

        [Column("FS_Effective_Date")]
        [JsonProperty("effectiveDate")]
        public DateTime EffectiveDate { get; set; }
        [Column("Employee_Status")]
        [JsonProperty("staffType")]
        public string StaffType { get; set; }
        [Column("FS_Entered_Emp_ID")]
        [JsonProperty("loginUserName")]
        public string LoginUserName { get; set; }
        [Column("Department_Code")]
        [JsonProperty("departmentCode")]
        public string DepartmentCode { get; set; }
        [Column("Sub_Department_Code")]
        [JsonProperty("subDepartmentCode")]
        public string SubDepartmentCode { get; set; }
        [Column("Designation_Code")]
        [JsonProperty("designationCode")]
        public string DesignationCode { get; set; }
        [Column("Area_Code")]
        [JsonProperty("areaCode")]
        public int? AreaCode { get; set; }
    }
}