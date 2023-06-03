using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;


namespace GherkinWebAPI.Models
{
    public class State
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty("stateCode")]
        public int State_Code { get; set; }

        [JsonProperty("stateName")]
        public string State_Name { get; set; }

        [JsonProperty("countryCode")]
        public int Country_Code { get; set; }

        //Navigation Properties
        public virtual Country Country { get; set; }
        [JsonProperty("districts")]
        public virtual ICollection<District> Districts { get; set; }
    }
}