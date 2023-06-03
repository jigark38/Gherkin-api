using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class CountryOverseas
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty("id")]
        public int Id { get; set; }
        [Key]
        [Column("W_Country_Id")]
        [JsonProperty("countryId")]
        public  string W_Country_Id { get; set; }

        [Column("W_Country_Name")]
        [JsonProperty("countryName")]
        [Required]
        [MaxLength(100)]
        public string W_Country_Name { get; set; }
       public  virtual ICollection<StateOverseas> StateOverseas { get; set; }
    }
}