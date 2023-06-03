using GherkinWebAPI.DTO.FeedInputTransfer;
using GherkinWebAPI.Models.PurchageMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.InputTransferDetails
{
    public class SaveStockAndBatchDetail
    {
        public InputTransferDetails rmInputTransferDetails { get; set; }
        public OutwardGatePassDetail outwardGatePassDetails { get; set; }
        public RMInputMaterialTransferDetail rMInputMaterialTransferDetails { get; set; }
    }
}