using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class SkillInformation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }
        [Key]
        [Column("Skills_Code")]
        public string skillsCode { get; set; }
        [Column("Skills_Name")]
        public string skillsName { get; set; }
        [Column("Department_Code")]
        public string departmentCode { get; set; }
    }
}