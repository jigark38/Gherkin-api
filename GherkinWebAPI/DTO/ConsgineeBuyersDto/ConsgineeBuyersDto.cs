using GherkinWebAPI.Models;
using GherkinWebAPI.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GherkinWebAPI.DTO
{
    public class ConsgineeBuyersDto
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("cbCode")]
        [Column("C_B_Code")]
        public string C_B_Code { get; set; }

        [Required]
        [MaxLength(10)]
        [Column("Cosng_Buyer_Type")]
        [JsonProperty("consgbuyerType")]
        public string Cosng_Buyer_Type { get; set; }

        [Column("C_B_Name")]
        [JsonProperty("cbName")]
        [Required]
        [MaxLength(50)]
        public string C_B_Name { get; set; }


        [Column("C_B_Address")]
        [JsonProperty("cbAddress")]
        [Required]
        [MaxLength(300)]
        public string C_B_Address { get; set; }

        [Column("W_Country_Id")]
        [JsonProperty("countryId")]
        [Required]
        [MaxLength(10)]
        public string W_Country_Id { get; set; }

        [JsonProperty("countryName")]
        public string W_Country_Name { get; set; }
        [Column("W_State_Id")]
        [JsonProperty("stateId")]
        [Required]
        [MaxLength(10)]
        public string W_State_Id { get; set; }
        [JsonProperty("stateName")]
        public string W_State_Name { get; set; }

        [Column("W_City_Id")]
        [JsonProperty("cityId")]
        [Required]
        [MaxLength(10)]
        public string W_City_Id { get; set; }
        [JsonProperty("cityName")]
        public string W_City_Name { get; set; }

        [Column("W_Pincode")]
        [JsonProperty("pinCode")]
        [Required]
        [MaxLength(10)]
        public string W_Pincode { get; set; }

        [Column("Country_Area_Code")]
        [JsonProperty("countryareaCode")]
        [Required]
        [MaxLength(10)]
        public string Country_Area_Code { get; set; }

        [Column("Managment_Name")]
        [JsonProperty("managmentName")]
        [Required]
        [MaxLength(12)]
        public string Managment_Name { get; set; }

        [Column("Mang_Mobile_No")]
        [JsonProperty("mobileNo")]
        [Required]
        [MaxLength(12)]
        public string Mang_Mobile_No { get; set; }

        [Column("Office_No")]
        [JsonProperty("officeNo")]
        [Required]
        [MaxLength(12)]
        public string Office_No { get; set; }

        [Column("Alternate_No")]
        [JsonProperty("alternateNo")]
        [Required]
        [MaxLength(12)]
        public string Alternate_No { get; set; }

        [Column("Mail_id")]
        [JsonProperty("mailId")]
        [EmailAddress(ErrorMessage = "Please provide valid email")]
        [Required]
        [MaxLength(50)]
        public string Mail_id { get; set; }

        [Column("Alt_Mail_id")]
        [JsonProperty("altmailId")]
        [Required]
        [MaxLength(50)]
        [EmailAddress(ErrorMessage = "Please provide valid alternate email")]
        public string Alt_Mail_id { get; set; }

        [Column("License_No")]
        [JsonProperty("licenseNo")]
        [Required]
        [MaxLength(30)]
        public string License_No { get; set; }

        [Column("Credit_Limited")]
        [JsonProperty("creditLimited")]
        [Required]
        [MaxLength(30)]
        public string Credit_Limited { get; set; }

        [Column("Currency_Code")]
        [JsonProperty("currencyCode")]
        [Required]
        [MaxLength(10)]
        public string Currency_Code { get; set; }
        [JsonProperty("createdDate")]
        public Nullable<DateTime> Created_Date { get; set; }


        [JsonProperty("modifiedDate")]
        public Nullable<DateTime> Modified_Date { get; set; }
        //[JsonProperty("documentName")]
        //public string Document_Name { get; set; }

        //[JsonProperty("documentDetails")]
        //public byte[] Document_Details { get; set; }

        [JsonProperty("currencyName")]
        public string Currency_Name { get; set; }
        public List<DcoumentUplodDto> documentUploadeds { get; set; }
    }
}