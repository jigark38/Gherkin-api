using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.MediaBatchDetails
{
    public class MediaStockAndBatchDetail
    {
        public string flag { get; set; }
        public DateTime stockDate { get; set; }
        public string stockNo { get; set; }
        public string orgOfficeNo { get; set; }
        public string rawMaterialGroupCode_A { get; set; }
        public string rawMaterialDetailsCode_A { get; set; }
        public string rawMaterialUOM { get; set; }
        public string rmStockTotalDetailQty { get; set; }
        public decimal rawMaterialTotalQty { get; set; }
        public decimal rawMaterialTotalAmount { get; set; }
        public DateTime rmStockLOTGRNDate { get; set; }
        public string rmStockLOTGRNNo { get; set; }
        public decimal rmStockLotGrnQty { get; set; }
        public decimal rmStockLotGrnRate { get; set; }
        public decimal rmStockLotGrnAmount { get; set; }
        public string rmTransferNo { get; set; }
        public DateTime rmTransferDate { get; set; }
        public decimal rmMaterialTransferQty { get; set; }
        public int rmBatchNo { get; set; }
        public DateTime rmGrnDate { get; set; }
        public string rmGrnNo { get; set; }
        public string rawmaterialGroupCode_B { get; set; }
        public string rawMaterialDetailsCode_B { get; set; }
        public decimal rmGRNreceivedQty { get; set; }
        public decimal rmGRNMaterialWiseTotalCost { get; set; }
        public decimal rmGRNMaterialWiseTotalRate { get; set; }

        public int issueQty { get; set; }
        //public string rmTransferNo { get; set; }
        //public DateTime rmTransferDate { get; set; }
        //public decimal rmMaterialTransferQty { get; set; }
    }
}