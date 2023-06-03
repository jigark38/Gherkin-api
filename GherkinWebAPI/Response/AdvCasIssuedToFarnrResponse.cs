using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Response
{
    public class AdvCasIssuedToFarnrResponse
    {
        public DateTime ACEntryDate { get; set; }
        public string ACEnteredEmployeeID { get; set; }
        public int ACIssuedNo { get; set; }
        public DateTime ACIssuedDate { get; set; }
        public string AreaID { get; set; }
        public string FieldSupervisorEmployeeID { get; set; }
        public string FieldStaffEmployeeID { get; set; }
        public string FarmersAccountNo { get; set; }
        public string FarmerCode { get; set; }
        public int AdvanceAmount { get; set; }
        public int VillageCode { get; set; }
        public string VillageName { get; set; }
        public string FarmerName { get; set; }
        public string FarmerAddress { get; set; }
        public int mandalCode { get; set; }
    }
}