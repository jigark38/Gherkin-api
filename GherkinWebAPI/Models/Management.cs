using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class Management
    {
        [Key]
        //[Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty("orgMngCode")]
        public int Org_Mng_Code { get; set; }

        //[ForeignKey]
        [Required]
        [JsonProperty("orgCode")]
        public int Org_Code { get; set; }
        
        [JsonProperty("managementDesignation")]
        public string Management_Designation { get; set; }


        [JsonProperty("managementName")]
        public string Management_Name { get; set; }

        
        
        [JsonProperty("orgMngContactNo")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Org_Mng_Contact_No { get; set; }

        
        [JsonProperty("orgMngAltContact_No")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Org_Mng_Alt_Contact_No { get; set; }


        [JsonProperty("orgMngEmailId")]
        public string Org_Mng_Email_Id { get; set; }

        [JsonProperty("orgMngResidenceDetails")]
        public string Org_Mng_Residence_Details { get; set; }

        //[RegularExpression(@"^[a - zA - Z]{5}[0-9]{4}[a-zA-Z]{1}$",ErrorMessage ="Enter valid PAN number")]
        [JsonProperty("orgMngPanDetails")]
        public string Org_Mng_Pan_Details { get; set; }

        [JsonProperty("orgMngDinDetails")]
        public string Org_Mng_Din_Details { get; set; }


    }

}