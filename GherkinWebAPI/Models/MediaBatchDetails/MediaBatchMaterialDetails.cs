using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.MediaBatchDetails
{

    [Table("Media_Batch_Materials_Details")]
    public class MediaBatchMaterialDetails
    {
        [Key]
        [Column("Batch_Material_Consumption_No")]
        public long BatchMaterialConsumptionNo { get; set; }
        [Column("Media_Batch_Production_No")]
        public long MediaBatchProductionNo { get; set; }
        [Column("BS_Order_Production_No")]
        public long BSOrderProductionNo { get; set; }
        [Column("Raw_Material_Group_Code")]
        public string RawMaterialGroupCode { get; set; }
        [Column("Raw_Material_Details_Code")]
        public string RawMaterialDetailsCode { get; set; }
        [Column("Stock_No")]
        public string StockNo { get; set; }
        [Column("RM_GRN_NO")]
        public string RMGRNNO { get; set; }
        [Column("RM_Stock_LOT_GRN_No")]
        public string RMStockLOTGRNNo { get; set; }
        [Column("RM_Stock_Lot_Grn_Rate")]
        public decimal? RMStockLotGrnRate { get; set; }
        [Column("RM_Batch_No")]
        public long? RMBatchNo { get; set; }
        [Column("RM_GRN_Materialwise_Total_Rate")]
        public decimal? RMGRNMaterialwiseTotalRate { get; set; }
        [Column("RM_Material_Transfer_Qty")]
        public decimal RMMaterialTransferQty { get; set; }
        [Column("RM_Material_Transfer_Amount")]
        public decimal RMMaterialTransferAmount { get; set; }


    }
}