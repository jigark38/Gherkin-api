using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.GRNAndMaterialClassification
{
    public class CWHarvestGRNWeightSummaryDetails
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("CW_Harvest_GRN_Weight_Summary_No")]
		public int CWHarvestGRNWeightSummaryNo { get; set; }
		[Column("Harvest_GRN_No")]
		public Int64 HarvestGRNNo { get; set; }
		[Column("Greens_Procurement_No")]
		public int GreensProcurementNo { get; set; }
		[Column("Buyer_Employee_ID")]
		public string BuyerEmployeeID { get; set; }
		[Column("No_of_Crates")]
		public int? NoofCrates { get; set; }
		[Column("CW_Countwise_Total_Quantity")]
		public decimal CWCountwiseTotalQuantity { get; set; }
	}
}