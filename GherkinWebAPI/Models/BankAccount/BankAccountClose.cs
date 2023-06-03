using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class BankAccountClose
    {
        [Key]
        [JsonProperty("id")]
        public int Bank_Account_Close_ID { get; set; }
        [JsonProperty("bankCode")]
        [Required(ErrorMessage = "Please enter Bank_Code")]
        public string Bank_Code { get; set; }
        [JsonProperty("accountCloseStatus")]
        [Required(ErrorMessage = "Please enter Account Status")]
        public string Bank_Account_Close_Status { get; set; }
        [JsonProperty("accountCloseReason")]
       // [Required(ErrorMessage = "Please enter Reason for Closing")]
        public string Bank_Account_Reasons { get; set; }
        [JsonProperty("accountClosingDate")]
        [Required(ErrorMessage = "Please enter Closing Date")]
        public DateTime Bank_Account_Closing_Date { get; set; }

    }
}