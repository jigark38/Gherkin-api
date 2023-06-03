using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.PurchageMgmt
{
    public class PurchageReturnMaterialDetail
    {
		[Column("Purchase_Return_No")]
		[Required]
		[MaxLength(10)]
		public string purchageReturnNo { get; set; }
		[Column("RM_Transfer_Date")]
		[Required]
		public DateTime rmTransferDate { get; set; } = DateTime.Now;
		[Column("Raw_Material_Group_Code")]
		[Required]
		[MaxLength(6)]
		public string rawMaterialGroupCode { get; set; }
		[Column("Raw_Material_Details_Code")]
		[Required]
		[MaxLength(7)]
		public string rawMaterialDetailsCode { get; set; }
		[Column("RM_GRN_NO")]
		[Required]
		[MaxLength(10)]
		public string rmGrnNo { get; set; }
		[Column("RM_Batch_No")]
		[Required]
		public int rmBatchNo { get; set; }
		[Column("RM_Materialwise_Return_Rate")]
		[Required]
		[Range(0, 99999999.99)]
		public Nullable<decimal> rmMaterialwiseReturnRate { get; set; }
		[Column("RM_Material_Return_Qty")]
		[Required]
		[Range(0, 99999999999.99)]
		public Nullable<decimal> rmMaterialReturnQty { get; set; }
		[Column("RM_Material_Return_Amount")]
		[Required]
		[Range(0, 9999999999999.99)]
		public Nullable<decimal> rmMaterialReturnAmount { get; set; }
		[Column("RM_GRN_IGST_Rate")]
		[Required]
		[Range(0, 99.99)]
		public Nullable<decimal> rmGrnIGSTRate { get; set; }
		[Column("RM_GRN_IGST_Reverse_Amount")]
		[Required]
		[Range(0, 99999999.99)]
		public Nullable<decimal> rmGrnIGSTReverseAmount { get; set; }
		[Column("RM_GRN_CGST_Rate")]
		[Required]
		[Range(0, 99.99)]
		public Nullable<decimal> rmGrnCGSTRate { get; set; }
		[Column("RM_GRN_CGST_Reverse_Amount")]
		[Required]
		[Range(0, 99999999.99)]
		public Nullable<decimal> rmGrnCGSTReverseAmount { get; set; }
		[Column("RM_GRN_SGST_Rate")]
		[Required]
		[Range(0, 99.99)]
		public Nullable<decimal> rmGrnSGSTRate { get; set; }
		[Column("RM_GRN_SGST_Reverse_Amount")]
		[Required]
		[Range(0, 99999999.99)]
		public Nullable<decimal> rmGrnSGSTReverseAmount { get; set; }
		[Column("ID")]
		[Key]
		public int Id { get; set; }
	}
}