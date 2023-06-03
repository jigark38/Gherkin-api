using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.ManualAttendence
{
    [Table("FS_Manual_Attendance_Details")]
    public class ManualAttendenceDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [JsonProperty("manualAttendanceID")]
        [Column("Manual_Attendance_ID")]
        public Int64 ManualAttendanceID { get; set; }

       
        [JsonProperty("manualAttendanceProcessID")]
        [Column("Manual_Attendance_Process_ID")]
        public Int64 ManualAttendanceProcessID { get; set; }

        [JsonProperty("manualAttendanceDate")]
        [Column("Manual_Attendance_Date")]
        public DateTime ManualAttendanceDate { get; set; }


        [JsonProperty("employeeID")]
        [Column("Employee_ID")]
        public string EmployeeID { get; set; }


        [JsonProperty("manualAttendanceStatus")]
        [Column("Manual_Attendance_Status")]
        public string ManualAttendanceStatus { get; set; }
    }
}