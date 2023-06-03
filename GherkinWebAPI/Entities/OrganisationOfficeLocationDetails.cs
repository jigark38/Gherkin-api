using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Entities
{
    public class OrganisationOfficeLocationDetails
    {
        [Key]
        [JsonProperty("orgOfficeNo")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Org_Office_No { get; set; }

        [JsonProperty("orgCode")]
        [Required]
        public int Org_Code { get; set; }

        [JsonProperty("orgOfficeName")]
        [Required]
        public string Org_Office_Name { get; set; }

        [JsonProperty("natureOfficeDetails")]
        public string Nature_Office_Details { get; set; }

        [JsonProperty("locationOfficeAddress")]
        public string Location_Office_Address { get; set; }

        [JsonProperty("countryName")]
        public string Country_Name { get; set; }

        [JsonProperty("stateName")]
        public string State_Name { get; set; }

        [JsonProperty("districtName")]
        public string District_Name { get; set; }

        [JsonProperty("placeName")]
        public string Place_Name { get; set; }

        [JsonProperty("locationPhoneDetails")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Location_Phone_Details { get; set; }

        [JsonProperty("locationFaxDetails")]
        public string Location_Fax_Details { get; set; }

        [JsonProperty("locationCellPhone")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]

        public string Location_Cell_Phone { get; set; }

        [JsonProperty("locationEmailId")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please Provide Valid Email")]

        public string Location_Email_Id { get; set; }

        [JsonProperty("laborLicenseNo")]
        public string Labor_License_No { get; set; }

        [JsonProperty("otherLicenseDetails")]
        public string Other_License_Details { get; set; }
    }

    public class OrganisationOfficeLocationDetailsResponse
    {
        public int OrgCode { get; set; }
        public int OrgOfficeNo { get; set; }
        public string OrgOfficeName { get; set; }
    }

    public class OrganisationOfficeLocationDTO
    {
        [Key]
        [JsonProperty("orgOfficeNo")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Org_Office_No { get; set; }

        [JsonProperty("orgCode")]
        [Required]
        public int Org_Code { get; set; }

        [JsonProperty("orgOfficeName")]
        [Required]
        public string Org_Office_Name { get; set; }

        [JsonProperty("natureOfficeDetails")]
        public string Nature_Office_Details { get; set; }

        [JsonProperty("locationOfficeAddress")]
        public string Location_Office_Address { get; set; }

        [JsonProperty("countryName")]
        public string Country_Name { get; set; }

        [JsonProperty("countryCode")]
        public int Country_Code { get; set; }

        [JsonProperty("stateName")]
        public string State_Name { get; set; }

        [JsonProperty("stateCode")]
        public int State_Code { get; set; }

        [JsonProperty("districtName")]
        public string District_Name { get; set; }

        [JsonProperty("districtCode")]
        public int District_Code { get; set; }

        [JsonProperty("placeName")]
        public string Place_Name { get; set; }

        [JsonProperty("placeCode")]
        public int Place_Code { get; set; }

        [JsonProperty("locationPhoneDetails")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Location_Phone_Details { get; set; }

        [JsonProperty("locationFaxDetails")]
        public string Location_Fax_Details { get; set; }

        [JsonProperty("locationCellPhone")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]

        public string Location_Cell_Phone { get; set; }

        [JsonProperty("locationEmailId")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please Provide Valid Email")]

        public string Location_Email_Id { get; set; }

        [JsonProperty("laborLicenseNo")]
        public string Labor_License_No { get; set; }

        [JsonProperty("otherLicenseDetails")]
        public string Other_License_Details { get; set; }
    }
}