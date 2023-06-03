using System;
namespace GherkinWebAPI.DTO.DriverDetail
{
    public class DriverDetailDTO
    {
        public string DriverID { get; set; }
        public string EmpCreatedID { get; set; }
        public DateTime DriverEntryDate { get; set; }
        public string EmployeeID { get; set; }
        public decimal DrivingYearsExp { get; set; }
        public string DriverLicenseType { get; set; }
        public string DriverLicenseNumber { get; set; }
        public DateTime DriverExpiryDate { get; set; }
        public string DrivingLicenseIssueAuthority { get; set; }
    }
}