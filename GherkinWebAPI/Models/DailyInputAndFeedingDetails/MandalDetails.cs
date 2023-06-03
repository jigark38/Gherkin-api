using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.DailyInputAndFeedingDetails
{
    [Table("Mandal_Details")]
    public class MandalDetails
    {
        [Key]
        [Column("Mandal_Code")]
        public string MandalCode { get; set; }
        [Column("Mandal_Name")]
        public string MandalName { get; set; }
        [Column("District_Code")]
        public string DistrictCode { get; set; }
        [Column("State_Code")]
        public string StateCode { get; set; }
        [Column("Country_Code")]
        public string CountryCode { get; set; }

    }
}