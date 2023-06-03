using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class SubDepartment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }
        [Key]
        [Column("Sub_Department_Code")]
        public string subDepartmentCode { get; set; }
        [Column("Sub_Department_Name")]
        public string subDepartmentName { get; set; }
        [Column("Department_Code")]
        public string departmentCode { get; set; }
    }
}