using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Request
{
    public class FarmerDetailsFilterModel
    {
        public string AreaId { get; set; }
        public string CropGroupCode { get; set; }
        public string CropNameCode { get; set; }
        public string PSNumber { get; set; }
        public string SupervisorFarmerCode { get; set; }
        public string FieldStaffFarmerCode { get; set; }
        public int? VillageCode { get; set; }
        public string FarmerCode { get; set; }
        public int? MandalCode { get; set; }
        public string FarmersAccountNo { get; set; }
    }
}