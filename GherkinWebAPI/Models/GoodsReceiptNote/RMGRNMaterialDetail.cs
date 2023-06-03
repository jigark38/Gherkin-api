using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.GoodsReceiptNote
{
    public class RMGRNMaterialDetail
    {

        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("RM_GRN_NO")]
        public string RMGRNNO { get; set; }
        [Column("RM_PO_NO")]
        public string RMPONO { get; set; }
        [Column("RM_GRN_Batch_No")]
        public int? RM_GRN_Batch_No { get; set; }
        [Column("Raw_Material_Group_Code")]
        public string RawMaterialGroupCode { get; set; }
        [Column("Raw_Material_Details_Code")]
        public string RawMaterialDetailsCode { get; set; }
        [Column("RM_GRN_Bill_Qty")]
        public decimal RMGRNBillQty { get; set; }
        [Column("RM_PO_Rate")]
        public decimal RMPORate { get; set; }
        [Column("RM_GRN_Bill_Rate")]
        public decimal RMGRNBillRate { get; set; }
        [Column("RM_GRN_Material_Wise_Cost")]
        public decimal RMGRNMaterialWiseCost { get; set; }
        [Column("RM_GRN_IGST_Rate")]
        public decimal? RMGRNIGSTRate { get; set; }
        [Column("RM_GRN_IGST_Amount")]
        public decimal? RMGRNIGSTAmount { get; set; }
        [Column("RM_GRN_CGST_Rate")]
        public decimal? RMGRNCGSTRate { get; set; }
        [Column("RM_GRN_CGST_Amount")]
        public decimal? RMGRNCGSTAmount { get; set; }
        [Column("RM_GRN_SGST_Rate")]
        public decimal? RMGRNSGSTRate { get; set; }
        [Column("RM_GRN_SGST_Amount")]
        public decimal? RMGRNSGSTAmount { get; set; }
        [Column("RM_GRN_Material_Wise_Total_Cost")]
        public decimal RMGRNMaterialWiseTotalCost { get; set; }
    }
}