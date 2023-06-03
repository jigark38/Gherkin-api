using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GherkinWebAPI.Models.Mandals;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models
{
    public class District
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty("districtCode")]
        public int District_Code { get; set; }
        [JsonProperty("districtName")]
        public string District_Name { get; set; }
        [JsonProperty("stateCode")]
        public int State_Code { get; set; }
        [JsonProperty("countryCode")]
        public int Country_Code { get; set; }

        //Navigation Properties
        //public virtual State State { get; set; }
        [JsonProperty("mandals")]
        public virtual ICollection<Mandal> Mandals { get; set; }
    }
}