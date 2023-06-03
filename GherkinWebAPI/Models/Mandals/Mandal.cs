using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Script.Serialization;

namespace GherkinWebAPI.Models.Mandals
{
    public class Mandal
    {
        [Key]
        [JsonProperty("mandalCode")]
        public int Mandal_Code { get; set; }
        [JsonProperty("mandalName")]
        public string Mandal_Name { get; set; }
        [JsonProperty("districtCode")]
        public int District_Code { get; set; }
        [JsonProperty("stateCode")]
        public int State_Code { get; set; }
        [JsonProperty("countryCode")]
        public int Country_Code { get; set; }

        //Navigation Properties
        //public virtual District District { get; set; }
        [JsonProperty("villages")]
        public virtual ICollection<Village> Villages { get; set; }
    }
}