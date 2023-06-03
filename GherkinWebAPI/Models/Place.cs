using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class Place
    {
        [Key]
        [Column("Place_Code")]
        public int PlaceCode { get; set; }

        [Column("Place_Name")]
        public string PlaceName { get; set; }

        [Column("State_Code")]
        public int StateCode { get; set; }

        [Column("Country_Code")]
        public int CountryCode { get; set; }

        [Column("District_Code")]
        public int DistrictCode { get; set; }
    }
}