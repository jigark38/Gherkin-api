using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.PurchageMgmt
{
    public class PurchageReceivedDetail
    {
        public DateTime grnDate { get; set; }
        public string rmGrnNo { get; set; }
        public DateTime invoiceDate { get; set; }
        public string invoiceNo { get; set; }
        public string invoiceType { get; set; }
        public Nullable<decimal> invoiceAmount { get; set; }
        public string gstType { get; set; }
        public string rawMaterialGroupCode { get; set; }

        public string rawMaterialDetailsCode { get; set; }
        public int rmGrnReceivedQty { get; set; }
        public int rmGrnMaterialTransferQty { get; set; }
        public Nullable<decimal> rmGrnMaterialWiseTotalCost { get; set; }
    }
    public class PurchageReceivedDetailResponse
    {
        public DateTime RM_GRN_Date { get; set; }
        public string RM_GRN_NO { get; set; }
        public DateTime Bill_DC_Date { get; set; }
        public string Bill_DC_No { get; set; }
        public Nullable<decimal> invoiceAmount { get; set; }
        public string Raw_Material_Group_Code { get; set; }
        public string GST_Type { get; set; }
        public string Invoice_DC_Type { get; set; }
        public string Raw_Material_Details_Code { get; set; }
        public Nullable<decimal> RM_GRN_Received_Qty { get; set; }
        public Nullable<decimal> RM_Material_Transfer_Qty { get; set; }
        public Nullable<decimal> RM_GRN_Material_Wise_Total_Cost { get; set; }
    }
}