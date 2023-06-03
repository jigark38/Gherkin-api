using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.Farmers
{
    public class FarmersDetail
    {
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("farmerCode")]
        public string FarmerCode { get; set; }
        [JsonProperty("dateOfEntry")]
        public DateTime DateOfEntry { get; set; }
        [JsonProperty("userName")]
        public string UserName { get; set; }
        [JsonProperty("farmerName")]
        public string FarmerName { get; set; }
        [JsonProperty("farmerAddress")]
        public string FarmerAddress { get; set; }
        [JsonProperty("countryCode")]
        public int CountryCode { get; set; }
        [JsonProperty("countryName")]
        public string CountryName { get; set; }
        [JsonProperty("stateCode")]
        public int StateCode { get; set; }
        [JsonProperty("stateName")]
        public string StateName { get; set; }
        [JsonProperty("districtCode")]
        public int DistrictCode { get; set; }
        [JsonProperty("districtName")]
        public string DistrictName { get; set; }
        [JsonProperty("mandalCode")]
        public int MandalCode { get; set; }
        [JsonProperty("mandalName")]
        public string MandalName { get; set; }
        [JsonProperty("villageCode")]
        public int VillageCode { get; set; }
        [JsonProperty("villageName")]
        public string VillageName { get; set; }
        [JsonProperty("pinCode")]
        public int PINCode { get; set; }
        [JsonProperty("alternativeContactPerson")]
        public string AlternativeContactPerson { get; set; }
        [JsonProperty("contactNumber")]
        public Int64 ContactNumber { get; set; }
        [JsonProperty("aadharCardNo")]
        public string AadharCardNo { get; set; }
        [JsonProperty("noOfAcres")]
        public decimal NoOfAcres { get; set; }
        [JsonProperty("bankName")]
        public string BankName { get; set; }
        [JsonProperty("bankBranch")]
        public string BankBranch { get; set; }
        [JsonProperty("bankAccountNo")]
        public string BankAccountNo { get; set; }
        [JsonProperty("bankIFSC")]
        public string BankIFSC { get; set; }
        [JsonProperty("approvedBy")]
        public string ApprovedBy { get; set; }

    }
}