using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.PurchageMgmt
{
    public class BranchIndentMaterialsDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("RM_Indent_No")]
        public string RmIndentNo { get; set; }
        [Column("Raw_Material_Group_Code")]
        public string RawMaterialGroupCode { get; set; }
        [Column("Raw_Material_Details_Code")]
        public string RawMaterialDetailsCode { get; set; }
        [Column("RM_Stock_On_Date")]
        public decimal RmStockOnDate { get; set; }
        [Column("RM_Indent_Req_Qty")]
        public decimal RmIndentReqQty { get; set; }
        [Column("RM_Require_Date")]
        public DateTime RmRequiredDate { get; set; }
        [Column("RM_Remarks")]
        public string RmRemarks { get; set; }

    }
}