using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GherkinWebAPI.Models;
using Newtonsoft.Json;

namespace GherkinWebAPI.DTO.BankAccount
{
    public class BankAccountDetailsDTO
    {
        //public BankAccountDetails BankAccountDetails { get; set; }
        //public BankAccountDetails AccountClose { get; set; } 
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("bankCode")]
        public string Bank_Code { get; set; }

        [JsonProperty("organisationCode")]
        public int Org_code { get; set; }
        [JsonProperty("dateOfEntry")]
        public DateTime Date_Of_Entry { get; set; }

        [JsonProperty("bankAccountNumber")]
        public string Bank_Account_Number { get; set; }
        [JsonProperty("bankName")]
        public string Bank_Name { get; set; }
        
        [JsonProperty("bankBranch")]
        public string Bank_Branch { get; set; }
        [JsonProperty("bankAddress")]
        public string Bank_Address { get; set; }

        [JsonProperty("bankIFSC")]
        public string Bank_IFSC { get; set; }
        
        [JsonProperty("bankSwiftCode")]
        public string Bank_Swift_Code { get; set; }
        
        [JsonProperty("bankOtherDetails")]
        public string Bank_Other_Details { get; set; }
        [JsonProperty("authorisationEmployee")]
        public string Bank_Authorised_Employee_ID { get; set; }
        [JsonProperty("operationDate")]
        public DateTime Bank_Operation_Date { get; set; }
        [JsonProperty("salaryLinkedAccount")]
        public string Bank_Salary_Link_Account { get; set; }

        [JsonProperty("accountCloseStatus")]
        public string Bank_Account_Close_Status { get; set; }

        //[JsonProperty("accountCloseReason")]
        //public string Bank_Account_Reasons { get; set; }

        //[JsonProperty("accountClosingDate")]
        //public DateTime Bank_Account_Closing_Date { get; set; }
    }
}