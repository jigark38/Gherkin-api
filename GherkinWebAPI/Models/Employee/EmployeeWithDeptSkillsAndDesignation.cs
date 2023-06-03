using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.Employee
{
    public class EmployeeWithDeptSkillsAndDesignation
    {
        public string employeeId { get; set; } = "0";
        public string employeeName { get; set; }
        public DateTime employeeCreationDate { get; set; }
        public string empCreatedId { get; set; }
        public Nullable<int> orgOfficeNo { get; set; }
        public string orgOfficeName { get; set; }
        public string contractorCode { get; set; }
        public int empBiometricId { get; set; }
        public string employeePicture { get; set; }
        public string employeeGender { get; set; }
        public string employeeDivision { get; set; }
        public DateTime employeeDOB { get; set; }
        public string employeeFatherSpouseName { get; set; }
        public string employeeRelationship { get; set; }
        public string employeeContactNo { get; set; }
        public string employeeAltContactNo { get; set; }
        public string employeeMailId { get; set; }
        public string employeeMaritalStatus { get; set; }
        public int? employeeNoOfDependents { get; set; }
        public string employeePresentAddress { get; set; }
        public string employeePermanentAddress { get; set; }
        public string employeeBloodGroup { get; set; }
        public string departmentCode { get; set; }
        public string departmenName { get; set; }
        public string subDepartmentCode { get; set; }
        public string subDepartmentName { get; set; }
        public string designationCode { get; set; }
        public string designationName { get; set; }
        public string skillsCode { get; set; }
        public string skillsName { get; set; }
        public DateTime employeeDOJ { get; set; }
        public decimal? employeeIHExp { get; set; }
        public decimal employeeTOTExp { get; set; }
        public string employeePFNo { get; set; }
        public string employeeEsiNo { get; set; }
        public string employeeAadharNo { get; set; }
        public string employeePassportNo { get; set; }
        public string employeePan { get; set; }
        public string employeeStatusAsOn { get; set; }
    }
}