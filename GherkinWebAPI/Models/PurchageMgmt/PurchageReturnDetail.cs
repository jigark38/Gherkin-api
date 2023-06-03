using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.PurchageMgmt
{
    public class PurchageReturnDetail
    {
		[Key]
		[Column("Purchase_Return_No")]
		[Required]
		[MaxLength(10)]
		public string purchageReturnNo { get; set; }
		[Column("Supplier_Org_ID")]
		[Required]
		[MaxLength(20)]
		public string supplierOrgId { get; set; }
		[Column("RM_GRN_No")]
		[Required]
		[MaxLength(10)]
		public string rmGrnNo { get; set; }
		[Column("GST_Type")]
		[Required]
		[MaxLength(10)]
		public string gstType { get; set; }
		[Column("PR_Material_Cost")]
		[Required]
		public int prMaterialCost { get; set; }
		[Column("PR_Packing_Amount")]
		[Required]
		public int prPackingAmount { get; set; }
		[Column("Packing_HSN_Code")]
		[Required]
		[MaxLength(20)]
		public string packingHsnCode { get; set; }
		[Column("Packing_Tax_Rate_Percentage")]
		[Required]
		[Range(0, 99.99)]
		public decimal packingTaxRateAmount { get; set; }
		[Column("PR_Packing_Tax_Amount")]
		[Required]
		public int prPackingTaxAmount { get; set; }
		[Column("PR_Freight_Amount")]
		[Required]
		public int prFreightAmount { get; set; }
		[Column("Freight_HSN_Code")]
		[Required]
		[MaxLength(20)]
		public string freightHsnCode { get; set; }
		[Column("Freight_Tax_Rate_Percentage")]
		[Required]
		[Range(0, 99.99)]
		public decimal freightTaxRatePercentage { get; set; }
		[Column("PR_Freight_Tax_Amount")]
		[Required]
		public int prFreightTaxAmount { get; set; }
		[Column("PR_Insurance_Amount")]
		[Required]
		public int prInsuranceAmount { get; set; }
		[Column("Insurance_HSN_Code")]
		[Required]
		[MaxLength(20)]
		public string insuranceHsnCode { get; set; }
		[Column("Insurance_Tax_Rate_Percentage")]
		[Required]
		[Range(0, 99.99)]
		public decimal insuranceTaxRatePercentage { get; set; }
		[Column("PR_Insurance_Tax_Amount")]
		[Required]
		public int prInsuranceTaxAmount { get; set; }
		[Column("PR_Total_Tax_Amount")]
		[Required]
		public int prTotalTaxAmount { get; set; }
		[Column("PR_Total_Amount")]
		[Required]
		public int prTotalAmount { get; set; }
		[Column("Purchase_Return_Date")]
		[Required]
		public DateTime purchaseReturnDate { get; set; } = DateTime.Now;
		[Column("PR_Remarks")]
		[Required]
		[MaxLength(300)]
		public string prRemarks { get; set; }
		[Column("Employee_ID")]
		[Required]
		public int employeeID { get; set; }
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("ID")]
		public int Id { get; set; }
	}
}