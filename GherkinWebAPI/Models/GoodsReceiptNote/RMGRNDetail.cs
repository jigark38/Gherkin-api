using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.GoodsReceiptNote
{
    public class RMGRNDetail
    {
        [Column("RM_GRN_Date")]
        public DateTime RMGRNDate { get; set; }
        [Key]
        [Column("RM_GRN_No")]
        public string RMGRNNo { get; set; }
        [Column("Inward_Gate_Pass_No")]
        public string InwardGatePassNo { get; set; }
        [Column("Domestic_Import")]
        public string DomesticImport { get; set; }
        [Column("Supplier_Org_ID")]
        public string SupplierOrgID { get; set; }
        public string Currency { get; set; }
        [Column("Total_Material_Cost")]
        public decimal TotalMaterialCost { get; set; }
        [Column("GST_Type")]
        public string GSTType { get; set; }
        [Column("Total_Tax_Amount")]
        public decimal TotalTaxAmount { get; set; }
        [Column("Total_MaterialCost_And_Tax_Amount")]
        public decimal TotalMaterialCostAndTaxAmount { get; set; }
        [Column("Customs_Amount")]
        public decimal CustomsAmount { get; set; }
        [Column("Packing_Amount")]
        public decimal? PackingAmount { get; set; }
        [Column("Packing_Tax_Rate_Percentage")]
        public decimal? PackingTaxRatePercentage { get; set; }
        [Column("Packing_Tax_Amount")]
        public decimal? PackingTaxAmount { get; set; }
        [Column("Freight_Amount")]
        public decimal? FreightAmount { get; set; }
        [Column("Freight_Tax_Rate_Percentage")]
        public decimal? FreightTaxRatePercentage { get; set; }
        [Column("Freight_Tax_Amount")]
        public decimal? FreightTaxAmount { get; set; }
        [Column("Insurance_Amount")]
        public decimal? InsuranceAmount { get; set; }
        [Column("Insurance_Tax_Rate_Percentage")]
        public decimal? InsuranceTaxRatePercentage { get; set; }
        [Column("Insurance_Tax_Amount")]
        public decimal? InsuranceTaxAmount { get; set; }
        [Column("Total_Bill_Amount")]
        public decimal TotalBillAmount { get; set; }
        [Column("Advance_Payment")]
        public decimal AdvancePayment { get; set; }
        [Column("Balance_Amount")]
        public decimal BalanceAmount { get; set; }
        [Column("Credit_Days")]
        public int CreditDays { get; set; }

        [Column("Discount_Percentage")]
        public decimal? DiscountPercentage { get; set; }
        [Column("Discount_Amount")]
        public decimal? DiscountAmount { get; set; }
        [Column("Invoice_DC_Type")]
        public string InvoiceDCType { get; set; }
        [Column("Bill_DC_Date")]
        public DateTime? BillDCDate { get; set; }
        [Column("Bill_DC_No")]
        public string BillDCNo { get; set; }
        [Column("Packing_HSN_Code")]
        public string PackingHSNCode { get; set; }
        [Column("Freight_HSN_Code")]
        public string FreightHSNCode { get; set; }
        [Column("Insurance_HSN_Code")]
        public string InsuranceHSNCode { get; set; }
    }
}