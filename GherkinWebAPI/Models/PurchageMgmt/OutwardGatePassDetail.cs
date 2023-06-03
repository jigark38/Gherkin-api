using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.PurchageMgmt
{
    public class OutwardGatePassDetail
    {
        [Column("OGP_NO")]
        [Key]
        public string ogpNo { get; set; }
        [Column("Transaction_No")]
        [MaxLength(20)]
        public string transactionNo { get; set; }
        [Column("OGP_Date")]
        [Required]
        public DateTime ogpDate { get; set; } = DateTime.Now;
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }
    }
}