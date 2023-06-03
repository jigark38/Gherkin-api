using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.GreenReception
{
    public class GreenInwardsDetail
    {
        public string inwardType { get; set; }
        public string areaId { get; set; }
        public long harvestGRNNo { get; set; }
        public DateTime inwardDateTime { get; set; }
        public string inwardGatePassNo { get; set; }
        public string supplierTransporterName { get; set; }
        public string invVehicleNo { get; set; }
        public string employeeNo { get; set; }
        public string supplierTransporterPlace { get; set; }
        public string invoiceDCNo { get; set; }
        public DateTime invoiceDCDate { get; set; }
    }
}