using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.DailyInputAndFeedingDetails
{
    public class DailyInputModel
    {
        public DateTime DateOfIssue { get; set; }
        public string AreaId { get; set; }
        public string EmployeeId { get; set; }
        public string CropGroup { get; set; }
        public string CropName { get; set; }
        public string PSnumber { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public int MandalId { get; set; }
        public int VillageId { get; set; }
        public FarmerAgreementDetails FarmerAgreementDetail { get; set; }
    }
}