using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.PurchageMgmt
{
    public class RMPOIndentDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        [Key]
        public int Id { get; set; }
        [Column("RM_PO_NO")]
        [MaxLength(20)]
        public string rmPoNo { get; set; }

        [Column("RM_Indent_No")]
        [MaxLength(7)]
        public string rmIndentNo { get; set; }
    }
}