using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.PurchageMgmt
{
    public class RMInputMaterialTransferDetail
    {
		[Key]
		[Column("ID")]
		public int Id { get; set; }
		[Column("RM_Transfer_No")]
		public string rmTransferNo { get; set; }
		[Column("HBOM_Practice_No")]
		public string hbomPracticeNo { get; set; }
		[Column("Raw_Material_Group_Code")]
		public string rawMaterialGroupCode { get; set; }
		[Column("Raw_Material_Details_Code")]
		public string rawMaterialDetailsCode { get; set; }
		[Column("OGP_NO")]
		public string ogpNo { get; set; }
		[Column("RM_Transfer_Date")]
		public DateTime rmTransferDate { get; set; }
		[Column("Stock_No")]
		public string stockno { get; set; }
		[Column("RM_Stock_LOT_GRN_No")]
		public string rmStockLotGrnNo { get; set; }
		[Column("RM_Stock_Lot_Grn_Rate")]
		public decimal rmStockLotGrnRate { get; set; }
		[Column("RM_GRN_NO")]
		public string rmGrnNo { get; set; }
		[Column("RM_Batch_No")]
		public int? rmBatchNo { get; set; }
		[Column("RM_GRN_Materialwise_Total_Rate")]
		public decimal? rmGrnMaterialwiseTotalRate { get; set; }
		[Column("RM_Material_Transfer_Amount")]
		public decimal rmMaterialTransferAmount { get; set; }
		[Column("RM_Material_Transfer_Qty")]
		public decimal rmMaterialTransferQty { get; set; }

	}
}