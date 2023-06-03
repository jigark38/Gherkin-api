using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.Employee
{
    public class EmployeeAttendanceUpdatedDetails
    {
        [Key]
        [Column("Attnd_Updated_No")]
        public int AttndUpdatedNo { get; set; } = 0;

        [Column("Org_office_No")]
        public int OrgOfficeNo { get; set; }

        [Column("Attendance_Date")]
        public DateTime AttendanceDate { get; set; }

        [Column("Entry_Updated_by_Employee_ID")]
        public string EntryUpdatedbyEmployeeID { get; set; }

        [Column("Attnd_Updated_Date")]
        public DateTime AttndUpdatedDate { get; set; }

        [Column("Employee_ID")]
        public string EmployeeID { get; set; }

        [Column("Sub_Department_Code")]
        public string SubDepartmentCode { get; set; }

        [Column("Attnd_In_Time_Updated")]
        public DateTime AttndInTime_Updated { get; set; }

        [Column("Attnd_In_Out_Updated")]
        public DateTime AttndInOutUpdated { get; set; }

        [Column("Duration")]
        public decimal Duration { get; set; }

        [Column("OThours")]
        public decimal OThours { get; set; }
    }
}