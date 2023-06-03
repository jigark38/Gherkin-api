using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.PurchageMgmt
{
    public class RMPOMaterialCondition
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        [Key]
        public int Id { get; set; }
        [Column("RM_PO_NO")]
        [MaxLength(20)]
        public string rmPoNo { get; set; }
        [Column("PO_MCNO")] 
        [MaxLength(10)]
        public string pOMcNo { get; set; }
        [Column("PO_MC")]
        [MaxLength(50)]
        public string poMc { get; set; }
        [Column("PO_MD")]
        [MaxLength(50)]
        public string poMd { get; set; }
        [Column("PO_MR")]
        [MaxLength(300)]
        public string poMr { get; set; }
    }
}