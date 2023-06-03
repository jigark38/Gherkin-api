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
    public class Employee
    {
        [Key]
        [Column("Employee_ID")]
        public string employeeId { get; set; } = "0";
        [MaxLength(50)]
        [Column("Employee_Name")]
        public string employeeName { get; set; }
        [Column("Employee_Creation_Date")]
        public DateTime employeeCreationDate { get; set; }
        [MaxLength(10)]
        [Column("Emp_Created_ID")]
        public string empCreatedId { get; set; }
        [MaxLength(10)]
        [Column("Contractor_Code")]
        public string contractorCode { get; set; }
        [Column("Emp_Biometric_ID")]
        public int empBiometricId { get; set; }
        [Column("Employee_Picture")]
        public string employeePicture { get; set; }
        [MaxLength(10)]
        [Column("Employee_Gender")]
        public string employeeGender { get; set; }
        [MaxLength(10)]
        [Column("Employee_Division")]
        public string employeeDivision { get; set; }
        [Column("Employee_DOB")]
        public DateTime employeeDOB { get; set; }
        [MaxLength(30)]
        [Column("Employee_Father_Spouse_Name")]
        public string employeeFatherSpouseName { get; set; }
        [MaxLength(20)]
        [Column("Employee_Relationship")]
        public string employeeRelationship { get; set; }
        //[Required]
        [Column("Employee_Contact_No")]
        //[MaxLength(10, ErrorMessage = "Employee contact number must be of 10 digit.")]
        //[MinLength(10, ErrorMessage = "Employee contact number is less than 10 digit. Please provide valid 10 digit contact number.")]
        public string employeeContactNo { get; set; }
        //[MaxLength(10, ErrorMessage = "Employee alternative contact number must be of 10 digit.")]
        [Column("Employee_Alt_Contatct_No")]
        public string employeeAltContactNo { get; set; }
        //[Required]
        [MaxLength(50)]
        //[EmailAddress(ErrorMessage = "Please provide valid employee email")]
        [Column("Employee_Mail_ID")]
        public string employeeMailId { get; set; }
        [MaxLength(10)]
        [Column("Employee_Marital_Status")]
        public string employeeMaritalStatus { get; set; }
        [Column("Employee_No_of_Dependents")]
        public int? employeeNoOfDependents { get; set; }
        [MaxLength(300)]
        [Column("Employee_Present_Address")]
        public string employeePresentAddress { get; set; }
        [MaxLength(300)]
        [Column("Employee_Permanent_Address")]
        public string employeePermanentAddress { get; set; }
        [MaxLength(20)]
        [Column("Employee_Blood_Group")]
        public string employeeBloodGroup { get; set; }
        [MaxLength(10)]
        [Column("Department_Code")]
        public string departmentCode { get; set; }
        [MaxLength(10)]
        [Column("Sub_Department_Code")]
        public string subDepartmentCode { get; set; }
        [MaxLength(10)]
        [Column("Designation_Code")]
        public string designationCode { get; set; }
        [MaxLength(10)]
        [Column("Skills_Code")]
        public string skillsCode { get; set; }
        [Column("Employee_DOJ")]
        public DateTime employeeDOJ { get; set; }
   
        [Column("Employee_IH_Exp")]
        public decimal? employeeIHExp { get; set; }

        [Column("Employee_TOT_Exp")]
        public decimal employeeTOTExp { get; set; }
        [MaxLength(25)]
        [Column("Employee_PF_No")]
        public string employeePFNo { get; set; }
        [MaxLength(25)]
        [Column("Employee_ESI_No")]
        public string employeeEsiNo { get; set; }
        [MaxLength(25)]
        [Column("Employee_Aadhar_No")]
        public string employeeAadharNo { get; set; }
        [MaxLength(30)]
        [Column("Employee_Passport_No")]
        public string employeePassportNo { get; set; }
        [MaxLength(20)]
        [Column("Employee_PAN")]
        public string employeePan { get; set; }
        [MaxLength(20)]
        [Column("Employment_Status_As_On")]
        public string employeeStatusAsOn { get; set; }
        [Column("ORG_Office_No")]
        public Nullable<int> orgOfficeNo { get; set; }
    }
}