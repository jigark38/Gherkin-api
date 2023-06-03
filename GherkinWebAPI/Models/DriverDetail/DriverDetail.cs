using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.DriverDetail
{
    public class DriverDetail
    {
        [Key]
        [Column("Driver_ID")]
        public int DriverID { get; set; }

        [Column("Emp_Created_ID")]
        public string EmpCreatedID { get; set; }

        [Column("Driver_Entry_Date")]
        public DateTime DriverEntryDate { get; set; }

        [Column("Employee_ID")]
        public string EmployeeID { get; set; }

        [Column("Driving_Years_Exp")]
        public decimal DrivingYearsExp { get; set; }

        [Column("Driver_License_Type")]
        public string DriverLicenseType { get; set; }

        [Column("Driver_License_Number")]
        public string DriverLicenseNumber { get; set; }

        [Column("Driver_Expiry_Date")]
        public DateTime DriverExpiryDate { get; set; }

        [Column("Driving_Licence_Issue_Authority")]
        public string DrivingLicenseIssueAuthority { get; set; }

    }
}