using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.PurchageMgmt
{
    public class PurchaseReceivedMaterialDetail
    {
        public string rmGrnNo { get; set; }
        public int rmBatchNo { get; set; }
        public string rawMaterialGroupCode { get; set; }
        public string rawMaterialDetailsCode { get; set; }
        public Nullable<decimal> rmGrnReceivedQty { get; set; }
        public Nullable<decimal> rmGrnMaterialwiseTotalCost { get; set; }
        public Nullable<decimal> rmCustomsShareAmount { get; set; }
        public Nullable<decimal> rmPackingShareAmount { get; set; }
        public Nullable<decimal> rmFreightShareAmount { get; set; }
        public Nullable<decimal> rmInsuranceShareAmount { get; set; }
        public Nullable<decimal> rmGrnMaterialTransferQty { get; set; }
        public Nullable<decimal> rmGrnMaterialBalanceQty { get; set; }
        public Nullable<decimal> rmGrnMaterialReturnQty { get; set; }
        public Nullable<int> rmGrnReturnMaterialCost { get; set; }
        public Nullable<int> rmGrnRateApply { get; set; }
        public string rawMaterialGroup { get; set; }
        public string rawMaterialDetailsName { get; set; }
        public Nullable<decimal> rmGrnBillQty { get; set; }
        public Nullable<decimal> rmGrnBillRate { get; set; }
        public Nullable<decimal> rmGrnIGSTRate { get; set; }
        public Nullable<decimal> rmGrnIGSTAmount { get; set; }
        public Nullable<decimal> rmGrnCGSTRate { get; set; }
        public Nullable<decimal> rmGrnCGSTAmount { get; set; }
        public Nullable<decimal> rmGrnSGSTRate { get; set; }
        public Nullable<decimal> rmGrnSGSTAmount { get; set; }
    }
}