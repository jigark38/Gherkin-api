using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.GreenReception
{
    public class GreensReceptionDetail
    {
        public long harvestGRNNo { get; set; }
        public DateTime harvestGrnDate { get; set; }
        public string areaId { get; set; }
        public Nullable<decimal> harvestGRNTotalQty { get; set; }
        public string vehicalNo { get; set; }
        public string cropNameCode { get; set; }
        public Nullable<decimal> farmerWiseTotalQty { get; set; }
    }
}