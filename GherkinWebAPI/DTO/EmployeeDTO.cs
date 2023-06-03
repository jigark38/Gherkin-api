using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO
{
    public class EmployeeDTO
    {
        public string employeeId { get; set; }
        public string employeeName { get; set; }
        public string departmentCode { get; set; }
        public string subDepartmentCode { get; set; }
        public string designationCode { get; set; }
    }
}