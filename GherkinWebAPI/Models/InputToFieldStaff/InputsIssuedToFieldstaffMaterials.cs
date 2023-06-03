using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.InputToFieldStaff
{
    [Table("Inputs_Issued_To_Fieldstaff_Materials")]
    public class InputsIssuedToFieldstaffMaterials
    {
        #region Instance Properties
        [Key]
        [Column("Material_FS_Issue ID")]
        public int MaterialFSIssueID { get; set; }

        [Column("Material_Issued_FS_No")]
        public string MaterialIssuedFSNo { get; set; }

        [Column("Inputs_Issued_FS_Date")]
        public DateTime InputsIssuedFSDate { get; set; }

        [Column("HBOM_Practice_No")]
        public string HBOMPracticeNo { get; set; }

        [Column("Raw_Material_Group_Code")]
        public string RawMaterialGroupCode { get; set; }

        [Column("Raw_Material_Details_Code")]
        public string RawMaterialDetailsCode { get; set; }

        [Column("Stock_No")]
        public string StockNo { get; set; }

        [Column("RM_Stock_LOT_GRN_No")]
        public string RMStockLOTGRNNo { get; set; }

        [Column("RM_Stock_Lot_Grn_Rate")]
        public decimal? RMStockLotGrnRate { get; set; }

        [Column("RM_GRN_NO")]
        public string RMGRNNO { get; set; }

        [Column("RM_Batch_No")]
        public int? RMBatchNo { get; set; }

        [Column("RM_GRN_Materialwise_Total_Rate")]
        public decimal? RMGRNMaterialwiseTotalRate { get; set; }

        [Column("FS_Material_Issued_Qty")]
        public decimal FSMaterialIssuedQty { get; set; }

        [Column("RM_Material_Transfer_Amount")]
        public decimal RMMaterialTransferAmount { get; set; }

        [Column("OGP_NO")]
        public string OGPNO { get; set; }

        #endregion Instance Properties

    }
}