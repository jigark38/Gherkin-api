using GherkinWebAPI.Models.GoodsReceiptNote;
using GherkinWebAPI.Models.PurchageMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.PurchageMgmt
{
    public class CreatePurchageReturn
    {
        public PurchageReturnDetail purchageReturnDetail { get; set; }
       // public List<RMGRNDetail> rmGrnDetail { get; set; }
        public List<OutwardGatePassDetail> outwardGatePassDetail { get; set; }
        public List<PurchageReturnMaterialDetail> purchageReturnMaterialDetail { get; set; }
    }
}