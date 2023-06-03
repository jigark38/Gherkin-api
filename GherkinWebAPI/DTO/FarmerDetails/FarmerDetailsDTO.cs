using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO
{
    public class FarmerDetailsDTO
    {
        public FarmerDetailsDTO()
        {
            FarmerBankDetails = new List<FarmerBankDetailsDTO>();
        }
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("farmerCode")]
        public string Farmer_Code { get; set; }
        [JsonProperty("dateOfEntry")]
        public DateTime DateOfEntry { get; set; }
        [JsonProperty("userName")]
        public string UserName { get; set; }
        [JsonProperty("farmerName")]
        public string FarmerName { get; set; }
        [JsonProperty("farmerAddress")]
        public string Farmer_Address { get; set; }
        [JsonProperty("countryCode")]
        public int Country_Code { get; set; }
        [JsonProperty("countryName")]
        public string CountryName { get; set; }
        [JsonProperty("stateCode")]
        public int State_Code { get; set; }
        [JsonProperty("stateName")]
        public string StateName { get; set; }
        [JsonProperty("districtCode")]
        public int District_Code { get; set; }
        [JsonProperty("districtName")]
        public string DistrictName { get; set; }
        [JsonProperty("mandalCode")]
        public int Mandal_Code { get; set; }
        [JsonProperty("mandalName")]
        public string MandalName { get; set; }
        [JsonProperty("villageCode")]
        public int Village_Code { get; set; }
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
        [JsonProperty("farmerBankDetails")]
        public List<FarmerBankDetailsDTO> FarmerBankDetails { get; set; }
    }

    public class FarmerBankDetailsDTO
    {
        [JsonProperty("farmerAccountHolderName")]
        public string Farmer_Account_Holder_Name { get; set; }
        [JsonProperty("farmerCode")]
        public string Farmer_Code { get; set; }
        [JsonProperty("farmerBankCode")]
        public int Farmer_Bank_Code { get; set; }
        [JsonProperty("farmerBankName")]
        public string Farmer_Bank_Name { get; set; }
        [JsonProperty("farmerBankBranch")]
        public string Farmer_Bank_Branch { get; set; }
        [JsonProperty("farmerBankAccountNo")]
        public int Farmer_Bank_Account_No { get; set; }
        [JsonProperty("farmerBankIFSC")]
        public string Farmer_Bank_IFSC { get; set; }
        [JsonProperty("preferredBank")]
        public string Preferred_Bank { get; set; }

    }
}