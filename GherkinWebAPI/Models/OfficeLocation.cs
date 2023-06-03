using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class OfficeLocation
    {
        [Key]
        [Required]
        public string Org_Office_No { get; set; }

        //[Required]
        public string Org_Code { get; set; }

        [Required]
        public string Org_Office_Name { get; set; }

        public string Nature_Office_Details { get; set; }

        public string Location_Office_Address { get; set; }

        public string Country_Code { get; set; }

        public string State_Code { get; set; }

        public string Place_Code { get; set; }

        [Phone]
        public int Location_Phone_Details { get; set; }
        [Phone]
        public int Location_Fax_Details { get; set; }

        [Phone]
        public int Location_Cell_Phone { get; set; }

        [EmailAddress]
        public string Location_Email_Id { get; set; }

        public string Labor_License_No { get; set; }

        public string Other_License_Details { get; set; }
    }
}