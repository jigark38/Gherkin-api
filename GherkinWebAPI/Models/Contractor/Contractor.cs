using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class Contractor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }
        [Key]
        [Column("Contractor_Code")]
        public string contractorCode { get; set; }
        [Column("Contractor_Name")]
        public string contractorName { get; set; }
        [Column("Contractor_Location")]
        public string contractorLocation { get; set; }
    }
}