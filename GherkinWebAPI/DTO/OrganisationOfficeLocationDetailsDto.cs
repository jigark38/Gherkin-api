using Newtonsoft.Json;

namespace GherkinWebAPI.DTO
{
    public class OrganisationOfficeLocationDetailsDto
    {
        [JsonProperty("OrgOfficeNo")]
        public int Org_Office_No { get; set; }

        [JsonProperty("OrgCode")]
        public int Org_Code { get; set; }

        [JsonProperty("OrgOfficeName")]
        public string Org_Office_Name { get; set; }

        [JsonProperty("NatureOfficeDetails")]
        public string Nature_Office_Details { get; set; }

        [JsonProperty("LocationOfficeAddress")]
        public string Location_Office_Address { get; set; }

        [JsonProperty("CountryName")]
        public string Country_Name { get; set; }

        [JsonProperty("StateCode")]
        public string State_Name { get; set; }

        [JsonProperty("PlaceCode")]
        public string Place_Name { get; set; }

        [JsonProperty("DistrictCode")]
        public string District_Name { get; set; }

        [JsonProperty("LocationPhoneDetails")]
        public string Location_Phone_Details { get; set; }

        [JsonProperty("LocationFaxDetails")]
        public string Location_Fax_Details { get; set; }

        [JsonProperty("LocationCellPhone")]
        public string Location_Cell_Phone { get; set; }

        [JsonProperty("LocationEmailId")]
        public string Location_Email_Id { get; set; }

        [JsonProperty("LaborLicenseNo")]
        public string Labor_License_No { get; set; }

        [JsonProperty("OtherLicenseDetails")]
        public string Other_License_Details { get; set; }
    }
}