using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GherkinWebAPI.Models.Mandals;

namespace GherkinWebAPI.Models
{
    public class Village
    {
        [Key]
        
        [JsonProperty("villageCode")]
        public int Village_Code { get; set; }
        [JsonProperty("villageName")]
        public string Village_Name { get; set; }
        [JsonProperty("mandalCode")]
        public int Mandal_Code { get; set; }
        [JsonProperty("districtCode")]
        public int District_Code { get; set; }
        [JsonProperty("stateCode")]
        public int State_Code { get; set; }
        [JsonProperty("countryCode")]
        public int Country_Code { get; set; }

        //Navigation Properties
       // public virtual Mandal Mandal { get; set; }
    }
}