using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.PurchageMgmt
{
    public class BranchIndentDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }
        [Column("RM_Indent_Entry_Date")]
        public DateTime RmIndentEntryDate { get; set; }
        [Column("RM_Indent_Emp_ID")]
        public string RmIndentEmpId { get; set; }
        [Column("RM_Indent_No")]
        public string RmIndentNo { get; set; }
        [Column("Area_ID")]
        public string AreaId { get; set; }
        [Column("Request_To")]
        public string RequestTo { get; set; }

    }
}