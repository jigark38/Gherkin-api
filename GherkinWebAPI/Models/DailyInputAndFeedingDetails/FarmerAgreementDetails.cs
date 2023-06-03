using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.DailyInputAndFeedingDetails
{
    public class FarmerAgreementDetails
    {
        [JsonProperty("farmersName")]
        public string Farmers_Name { get; set; }
        [JsonProperty("farmersAgreementCode")]
        public string Farmers_Agreement_Code { get; set; }
        [JsonProperty("farmerCode")]
        public string Farmer_Code { get; set; }
        [JsonProperty("farmersNoofAcresArea")]
        public decimal Farmers_No_of_Acres_Area { get; set; }
        [JsonProperty("agricultureDripNonDrip")]
        public string Agriculture_DRIP_NONDRIP { get; set; }
        [JsonProperty("villageName")]
        public string VillageName { get; set; }
        [JsonProperty("accountNumber")]
        public string AccountNumber { get; set; }
    }
}