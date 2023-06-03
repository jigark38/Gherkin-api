using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class BankAccountDetails
    {
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("bankCode")]
        public string Bank_Code { get; set; }
        [JsonProperty("organisationCode")]
        public int Org_code { get; set; }
        [Required(ErrorMessage = "Please enter Date Of Entry")]
        [JsonProperty("dateOfEntry")]
        public DateTime Date_Of_Entry { get; set; }
        [Required(ErrorMessage = "Please enter Bank Account Number")]
        [MaxLength(30, ErrorMessage = "Maximum length for Bank Account is 30")]
        [RegularExpression("(0[0-9]+)", ErrorMessage = "Please enter valid Bank Account Number")]
        [JsonProperty("bankAccountNumber")]
        public string Bank_Account_Number { get; set; }
        [Required(ErrorMessage = "Please enter Name of Bank")]
        [JsonProperty("bankName")]
        public string Bank_Name { get; set; }
        [Required(ErrorMessage = "Please enter Bank Branch")]
        [JsonProperty("bankBranch")]
        public string Bank_Branch{ get; set; }
        [Required(ErrorMessage = "Please enter Bank Address")]
        [MaxLength(200)]
        [JsonProperty("bankAddress")]
        public string Bank_Address { get; set; }
        [Required(ErrorMessage = "Please enter Bank IFSC Code")]
        [MaxLength(11, ErrorMessage = "Maximum length for IFSC code can be 11")]
        [JsonProperty("bankIFSC")]
        public string Bank_IFSC { get; set; }
        [Required(ErrorMessage = "Please enter Bank SWIFT Code")]
        [MaxLength(30)]
        [JsonProperty("bankSwiftCode")]
        public string Bank_Swift_Code { get; set; }
       // [Required(ErrorMessage = "Please enter Bank Other Details")]
        [MaxLength(200)]
        [JsonProperty("bankOtherDetails")]
        public string Bank_Other_Details { get; set; }
        [Required(ErrorMessage = "Please enter details for Bank Authorised Employee")]
        [JsonProperty("authorisationEmployee")]
        public string Bank_Authorised_Employee_ID { get; set; }
        [JsonProperty("operationDate")]
        public DateTime Bank_Operation_Date { get; set; }
        [Required(ErrorMessage = "Please confirm if Salary Linked Account or Not")]
        [JsonProperty("salaryLinkedAccount")]
        public string Bank_Salary_Link_Account { get; set; }

    }
}