using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.ManualAttendence
{
    [Table("FS_Manual_Attendance_Master")]
    public class ManualAttendenceMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [JsonProperty("manualAttendanceProcessID")]
        [Column("Manual_Attendance_Process_ID")]
        public Int64 ManualAttendanceProcessID { get; set; }

        [JsonProperty("orgOfficeNo")]
        [Column("Org_Office_No")]
        public string OrgOfficeNo { get; set; }

        [JsonProperty("passingManualAttendanceDate")]
        [Column("Passing_Manual_Attendance_Date")]
        public DateTime PassingManualAttendanceDate { get; set; }
    }
}