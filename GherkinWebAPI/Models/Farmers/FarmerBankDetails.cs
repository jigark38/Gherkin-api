using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class FarmerBankDetails
    {
        [Required]
        [Column("Farmer_Code")]
        [JsonProperty("farmerCode")]
        [MaxLength(7)]
        public string Farmer_Code { get; set; }        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Farmer_Bank_Code")]
        [JsonProperty("farmerBankCode")]
        public int Farmer_Bank_Code { get; set; }
        [Required]
        [Column("Farmer_Account_Holder_Name")]
        [JsonProperty("farmerAccountHolderName")]
        [MaxLength(30)]
        public string Farmer_Account_Holder_Name { get; set; }
        [Required]
        [Column("Farmer_Bank_Name")]
        [JsonProperty("farmerBankName")]
        [MaxLength(50)]
        public string Farmer_Bank_Name { get; set; }
        [Required]
        [Column("Farmer_Bank_Branch")]
        [JsonProperty("farmerBankBranch")]
        [MaxLength(30)]
        public string Farmer_Bank_Branch { get; set; }
        [Column("Farmer_Bank_Account_No")]
        [JsonProperty("farmerBankAccountNo")]
        public int Farmer_Bank_Account_No { get; set; }
        [Required]
        [Column("Farmer_Bank_IFSC")]
        [JsonProperty("farmerBankIFSC")]
        [MaxLength(15)]
        public string Farmer_Bank_IFSC { get; set; }
        [Required]
        [Column("Preferred_Bank")]
        [JsonProperty("preferredBank")]
        [MaxLength(10)]
        public string Preferred_Bank { get; set; }
}
}