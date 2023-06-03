using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class HarvestAreaBuyingStaffDetails
    {
        [Key]
        [Column("Id")]
        [JsonProperty("id")]
        public int Id { get; set; }
        [Column("BS_Entry_Date")]
        [JsonProperty("bsEntryDate")]
        public DateTime BSEntryDate { get; set; }
        [Column("BS_Entered_Emp_ID")]
        [JsonProperty("bsEnteredEmpID")]
        public string BSEnteredEmpID { get; set; }
        [Column("Employee_Status")]
        [JsonProperty("employeeStatus")]
        public string Employee_Status { get; set; }
        [Column("Employee_ID")]
        [JsonProperty("employeeID")]
        public string EmployeeID { get; set; }
        [Column("BS_Effective_Date")]
        [JsonProperty("bsEffectiveDate")]
        public DateTime BSEffectiveDate { get; set; }
        [Column("Area_ID")]
        [JsonProperty("areaID")]
        public string AreaID { get; set; }
    }
}