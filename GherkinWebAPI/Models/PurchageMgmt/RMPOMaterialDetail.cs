using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.PurchageMgmt
{
    public class RMPOMaterialDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        [Key]
        public int Id { get; set; }
        [Column("RM_PO_NO")]
        [MaxLength(20)]
        public string rmPoNo { get; set; }
        [Column("Raw_Material_Group_Code")]
        [Required]
        [MaxLength(10)]
        public string rawMaterialGroupCode { get; set; }
        [Column("Raw_Material_Details_Code")]
        [Required]
        [MaxLength(10)]
        public string rowMaterialDetailsCode { get; set; }
        [Column("RM_Order_Qty")]
        [Range(0, 9999999.999)]
        public decimal rmOrderQty { get; set; }
        [Column("RM_Quotation_Date")]
        public Nullable<DateTime> rmQuotationDate { get; set; } = DateTime.Now;
        [Column("RM_Quotation_No")]
        [MaxLength(30)]
        public string rmQuotationNo { get; set; }
        [Column("RM_Quotation_Rate")]
        [Range(0, 99999999.99)]
        public Nullable<decimal> rmQuotationRate { get; set; }
        [Column("RM_PO_Rate")]
        [Range(0, 99999999.99)]
        public Nullable<decimal> rmPoRate { get; set; }
        [Column("RM_PO_Material_Wise_Cost")]
        [Range(0, 9999999999999.99)]
        public Nullable<decimal> rmPoMaterialWiseCost { get; set; }
        [Column("RM_PO_Material_IGST_Rate")]
        [Range(0, 99.99)]
        public Nullable<decimal> rmPoMaterialIGSTRate { get; set; }
        [Column("RM_PO_Material_CGST_Rate")]
        [Range(0, 99.99)]
        public Nullable<decimal> rmPoMaterialCGSTRate { get; set; }
        [Column("RM_PO_Material_SGST_Rate")]
        [Range(0, 99.99)]
        public Nullable<decimal> rmPoMaterialSGSTRate { get; set; }
        [Column("RM_PO_Material_IGST_Amount")]
        [Range(0, 99999999.99)]
        public Nullable<decimal> rmPoMaterialIGSTAmount { get; set; }
        [Column("RM_PO_Material_CGST_Amount")]
        [Range(0, 99999999.99)]
        public Nullable<decimal> rmPoMaterialCGSTAmount { get; set; }
        [Column("RM_PO_Material_SGST_Amount")]
        [Range(0, 99999999.99)]
        public Nullable<decimal> rmPoMaterialSGSTAmount { get; set; }
        [Column("RM_PO_Material_Wise_Total_Cost")]
        [Range(0, 99999999999999.99)]
        public Nullable<decimal> rmPoMaterialWiseTotalCost { get; set; }
    }
}