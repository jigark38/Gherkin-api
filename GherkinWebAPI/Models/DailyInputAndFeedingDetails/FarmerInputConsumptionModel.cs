using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.DailyInputAndFeedingDetails
{
    public class FarmerInputConsumptionModel
    {
        
        public int MIFConsumptionNo { get; set; }

        public DateTime MIFDateofIssue { get; set; }

        public string FarmerCode { get; set; }
        public List<FarmersMaterialIssueDetails> ListFarmer { get; set; }
    }
}