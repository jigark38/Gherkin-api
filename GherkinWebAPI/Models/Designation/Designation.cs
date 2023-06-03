using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class Designation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }
        [Key]
        [Column("Designation_Code")]
        public string designationCode { get; set; }
        [Column("Designation_Name")]
        public string designattionName { get; set; }
        [Column("Department_Code")]
        public string departmentCode { get; set; }
        [Column("Sub_Department_Code")]
        public string subDepartmentCode { get; set; }
        //  public virtual ICollection<Employee> Employees { get; set; }
    }
}