using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class Organisation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty("orgCode")]
        //[IgnoreDataMember]
        //[Required]
        //[RegularExpression(@"[A-Z]{3,50}$",ErrorMessage = "Only uppercase Characters are allowed.")]
        public int Org_Code { get; set; }


        [JsonProperty("organisationName")]
        public string Organisation_Name { get; set; }

        [JsonProperty("orgStatus")]
        public string Org_Status { get; set; }

        [Required(ErrorMessage ="Certificate details are mandatory")]
        [JsonProperty("regCertificateDetails")]
        public string Reg_Certificate_Details { get; set; }

        [JsonProperty("certificationNo")]
        public string Certification_No { get; set; }

        [JsonProperty("otherCertificateNo")]
        public string Other_Certificate_No { get; set; }

        [Required(ErrorMessage = "org email is mandatory")]
        [EmailAddress]
        [JsonProperty("orgMngEmailId")]
        public string Org_Mng_Email_Id { get; set; }

        //[Url]
        [JsonProperty("websiteDetails")]
        public string Website_Details { get; set; }
    }
}