using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.Employee
{
    public class EmployeeDocument
    {
        [Key]
        [Column("Document_Id")]
        public int docId { get; set; }
        [Column("Employee_ID")]
        public string employeeId { get; set; }
        [Column("Employee_Document_Name")]
        public string employeeDocName { get; set; }
        [Column("Employee_Document_Details")]
        public byte[] employeeDocDetails { get; set; }
    }
}