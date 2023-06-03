using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models
{
    public class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty("countryCode")]
        public int Country_Code { get; set; }

        [JsonProperty("countryName")]
        public string Country_Name { get; set; }

        //Navigation Properties
        [JsonProperty("states")]
        public virtual ICollection<State> States { get; set; }

    }
}