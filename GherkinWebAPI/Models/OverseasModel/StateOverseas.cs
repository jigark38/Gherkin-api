using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class StateOverseas
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty("id")]
        public int ID { get; set; }
        [Key]
        [Column("W_State_id")]
        [JsonProperty("stateId")]
        public string W_State_id { get; set; }
        [Required]
        [MaxLength(100)]
        [Column("W_State_Name")]
        [JsonProperty("stateName")]
        public string W_State_Name { get; set; }
        [JsonProperty("countryId")]
        [Column("W_Country_Id")]
        [Required]
        [MaxLength(10)]
        public string W_Country_Id { get; set; }
        public virtual ICollection<CityOverseas> CityOverseas { get; set; }

    }
}