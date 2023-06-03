using System;

namespace GherkinWebAPI.Models.GoodsReceiptNote
{
    public class InwardDetail
    {
        public string InwardType { get; set; }
        public DateTime? InwardDateTime { get; set; }
        public string InwardGatePassNo { get; set; }
        public string SupplierTransporterName { get; set; }
        public string SupplierTransporterPlace { get; set; }
        public string InvDCNo { get; set; }
        public DateTime? InvDCDate { get; set; }
        public string ReceivedMaterialName { get; set; }
        public decimal? ReceivedQuantity { get; set; }
        public string QCTest { get; set; }
    }
}