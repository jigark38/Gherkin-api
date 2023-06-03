using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.FeedInputTransfer
{
    public class RMInputMaterialTransDetailsDto
    {
        public string rMTransferNo { get; set; }
        public string hBOMPracticeNo { get; set; }
        public string rawMaterialGroupCode { get; set; }
        public string rawMaterialDetailsCode { get; set; }
        public string oGPNO { get; set; }
        public DateTime rMTransferDate { get; set; }
        public string stockNo { get; set; }
        public string rMStockLOTGRNNo { get; set; }
        public decimal rMStockLotGrnRate { get; set; }
        public string rMGRNNO { get; set; }
        public int rMBatchNo { get; set; }
        public decimal rMGRNMaterialwiseTotalRate { get; set; }
        public decimal rMMaterialTransferAmount { get; set; }
        public decimal rMMaterialTransferQty { get; set; }
    }
}