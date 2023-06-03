using System;
using System.Collections.Generic;

namespace GherkinWebAPI.Models.GoodsReceiptNote
{
    public class OrderDetail
    {
        public DateTime RMGRNDate { get; set; }
        public string RMGRNNo { get; set; }
        public string InwardGatePassNo { get; set; }
        public string DomesticImport { get; set; }
        public string SupplierOrgID { get; set; }
        public string Currency { get; set; }
        public decimal TotalMaterialCost { get; set; }
        public string GSTType { get; set; }
        public decimal TotalTaxAmount { get; set; }
        public decimal TotalMaterialCostAndTaxAmount { get; set; }
        public decimal CustomsAmount { get; set; }
        public decimal? PackingAmount { get; set; }
        public decimal? PackingTaxRatePercentage { get; set; }
        public decimal? PackingTaxAmount { get; set; }
        public decimal? FreightAmount { get; set; }
        public decimal? FreightTaxRatePercentage { get; set; }
        public decimal? FreightTaxAmount { get; set; }
        public decimal? InsuranceAmount { get; set; }
        public decimal? InsuranceTaxRatePercentage { get; set; }
        public decimal? InsuranceTaxAmount { get; set; }
        public decimal TotalBillAmount { get; set; }
        public decimal AdvancePayment { get; set; }
        public decimal BalanceAmount { get; set; }
        public int CreditDays { get; set; }
        public string InvoiceDCType { get; set; }
        public DateTime? BillDCDate { get; set; }
        public string BillDCNo { get; set; }
        public string PackingHSNCode { get; set; }
        public string FreightHSNCode { get; set; }
        public string InsuranceHSNCode { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountAmount { get; set; }
        public List<OrderMaterialDetail> OrderMaterialDetails { get; set; }
        public List<OrderMaterialTotalCostDetail> OrderMaterialTotalCostDetails { get; set; }
    }

    public class OrderMaterialDetail
    {
        public int Id { get; set; }
        public string RMGRNNO { get; set; }
        public string RMPONO { get; set; }
        public int? RMGRNBatchNo { get; set; }
        public string RawMaterialGroupCode { get; set; }
        public string RawMaterialDetailsCode { get; set; }
        public decimal RMGRNBillQty { get; set; }
        public decimal RMPORate { get; set; }
        public decimal RMGRNBillRate { get; set; }
        public decimal RMGRNMaterialWiseCost { get; set; }
        public decimal? RMGRNIGSTRate { get; set; }
        public decimal? RMGRNIGSTAmount { get; set; }
        public decimal? RMGRNCGSTRate { get; set; }
        public decimal? RMGRNCGSTAmount { get; set; }
        public decimal? RMGRNSGSTRate { get; set; }
        public decimal? RMGRNSGSTAmount { get; set; }
        public decimal RMGRNMaterialWiseTotalCost { get; set; }

        public string SupplierOrganisationName { get; set; }
        public string RawMaterialDetaisName { get; set; }
        public decimal? RMOrderQty { get; set; }
        public decimal? TillNowRecordQuantity { get; set; }
        public decimal? RMPOMaterialIGSTRate { get; set; }
        public decimal? RMPOMaterialCGSTRate { get; set; }
        public decimal? RMPOMaterialSGSRate { get; set; }

        public DateTime? PODate { get; set; }


    }

    public class OrderMaterialTotalCostDetail
    {
        public int Id { get; set; }
        public string RMGRNNO { get; set; }
        public string RawMaterialGroupCode { get; set; }
        public string RawMaterialDetailsCode { get; set; }
        public decimal RMGRNMaterialWiseTotalCost { get; set; }
        public int RMBatchNo { get; set; }
        public decimal RMGRNReceivedQty { get; set; }
        public decimal? RMCustomsShareAmount { get; set; }
        public decimal? RMPackingShareAmount { get; set; }
        public decimal? RMFreightShareAmount { get; set; }
        public decimal? RMInsuranceShareAmount { get; set; }
        public decimal RMGRNMaterialwiseTotalCost { get; set; }
        public decimal RMGRNMaterialwiseTotalRate { get; set; }

    }

}