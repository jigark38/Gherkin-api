using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class Farmer
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty("id")]
        public int ID { get; set; }
        [Key]
        [JsonProperty("farmerCode")]
        public string Farmer_Code { get; set; }
        [Required]
        [Column("Farmer_Creation_Date")]
        [JsonProperty("dateOfEntry")]
        public DateTime DateOfEntry { get; set; }
        [Required]
        [MaxLength(10)]
        [Column("Emp_Created_ID")]
        [JsonProperty("userName")]
        public string UserName { get; set; }
        [Required]
        [MaxLength(100)]
        [Column("Farmer_Name")]
        [JsonProperty("farmerName")]
        public string FarmerName { get; set; }
        [Required]
        [MaxLength(200)]
        [JsonProperty("farmerAddress")]
        public string Farmer_Address { get; set; }
        [Required]
        [JsonProperty("countryCode")]
        public int Country_Code { get; set; }
        [Required]
        [JsonProperty("stateCode")]
        public int State_Code { get; set; }
        [Required]
        [JsonProperty("districtCode")]
        public int District_Code { get; set; }
        [Required]
        [JsonProperty("mandalCode")]
        public int Mandal_Code { get; set; }
        [Required]
        [Column("Place_Code")]
        [JsonProperty("villageCode")]
        public int Village_Code { get; set; }
        [Required]
        [Column("Farmer_PIN_Code")]
        [JsonProperty("pinCode")]
        public int PINCode { get; set; }
        [Required]
        [MaxLength(50)]
        [Column("Farmer_Altr_Contact_Person")]
        [JsonProperty("alternativeContactPerson")]
        public string AlternativeContactPerson { get; set; }
        [Column("Farmer_Contact_Number")]
        [JsonProperty("contactNumber")]
        public Int64 ContactNumber { get; set; }
        [Required]
        [MaxLength(15)]
        [Column("Farmer_Aadhar_Card_No")]
        [JsonProperty("aadharCardNo")]
        public string AadharCardNo { get; set; }
        [Required]
        [Column("Farmer_Land_No_Of_Acres")]
        [JsonProperty("noOfAcres")]
        public decimal NoOfAcres { get; set; }
        [MaxLength(1)]
        [Column("Farmer_Bank_Name")]
        [JsonProperty("bankName")]
        public string BankName { get; set; }
        [MaxLength(1)]
        [Column("Farmer_Bank_Branch")]
        [JsonProperty("bankBranch")]
        public string BankBranch { get; set; }
        [Column("Farmer_Bank_Account_No")]
        [JsonProperty("bankAccountNo")]
        public string BankAccountNo { get; set; }
        [MaxLength(15)]
        [Column("Farmer_Bank_IFSC")]
        [JsonProperty("bankIFSC")]
        public string BankIFSC { get; set; }
        [Column("Farmer_Approved_By_Emp_ID")]
        [JsonProperty("approvedBy")]
        public string ApprovedBy { get; set; }

    }
}