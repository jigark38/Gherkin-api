using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.MediaBatchDetails
{
    public class PendingOrderScheduleGrid
    {
        public string PreserveIn { get; set; }
        public DateTime OrderDate { get; set; }
        public long OrderNo { get; set; }
        public string ProductName { get; set; }
        public string Grade { get; set; }
        public long TotalQty { get; set; }
        public DateTime ReqDate { get; set; }
        public string MediaProcessCode { get; set; }
        public long BSOrderProdNo { get; set; }
    }
}