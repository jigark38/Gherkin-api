using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class CityOverseas
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty("id")]
        public int Id { get; set; }
        [Key]

        [Column("W_City_Id")]
        [JsonProperty("cityId")]
        public string W_City_Id { get; set; }
        [Required]
        [MaxLength(50)]
        [Column("W_City_Name")]
        [JsonProperty("cityName")]
        public string W_City_Name { get; set; }
        [Column("W_State_id")]
        [JsonProperty("stateId")]
        [Required]
        [MaxLength(10)]
        public string W_State_id { get; set; }
        [Column("W_Country_Id")]
        [JsonProperty("countryId")]
        [Required]
        [MaxLength(10)]
        public string W_Country_Id { get; set; }

    }
}