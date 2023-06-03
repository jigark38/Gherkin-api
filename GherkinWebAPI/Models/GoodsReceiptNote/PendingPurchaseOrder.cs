using System;

namespace GherkinWebAPI.Models.GoodsReceiptNote
{
    public class PendingPurchaseOrder
    {
        public DateTime? RMPODate { get; set; }
        public string RMPONo { get; set; }
        public string RawMaterialGroupCode { get; set; }
        public string RawMaterialDetailsCode { get; set; }
        public string RawMaterialDetaisName { get; set; }
        public string SupplierOrgId { get; set; }
        public string SupplierOrganisationName { get; set; }
        public string PlaceName { get; set; }
        public string DomesticImport { get; set; }
        public string GSTType { get; set; }
        public decimal? RMOrderQty { get; set; }
        public decimal? TillNowRecordQuantity { get; set; }
        public decimal? PendingQuatity { get; set; }
        public decimal? RMPORate { get; set; }
        public decimal? RMPOMaterialWiseCost { get; set; }
        public decimal? RMPOMaterialIGSTRate { get; set; }
        public decimal? RMPOMaterialCGSTRate { get; set; }
        public decimal? RMPOMaterialSGSRate { get; set; }
        public string RMGRNNO { get; set; }

    }
}