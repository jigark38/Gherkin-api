using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.GRNAndMaterialClassification
{
    public class CWHarvestBuyerWeighingDetails
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("Buyerwise_Centre_Weighing_No")]
		public int BuyerwiseCentreWeighingNo { get; set; }
		[Column("Harvest_GRN_No")]
		public Int64? HarvestGRNNo { get; set; }
		[Column("Greens_Procurement_No")]
		public int? GreensProcurementNo { get; set; }
		[Column("Buyer_Employee_ID")]
		public string BuyerEmployeeID { get; set; }
		[Column("Crop_Group_Code")]
		public string CropGroupCode { get; set; }
		[Column("Crop_Name_Code")]
		public string CropNameCode { get; set; }
		[Column("Crop_Scheme_Code")]
		public string CropSchemeCode { get; set; }
		[Column("No_of_Crates")]
		public int? NoofCrates { get; set; }
		[Column("CW_Countwise_Total_Quantity")]
		public decimal CWCountwiseTotalQuantity { get; set; }
	}
}