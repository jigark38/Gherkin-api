using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace GherkinWebAPI.Request
{
    public class RMUom
    {
        [Key]
        [JsonProperty("RawMaterialUOM")]
        public string Raw_Material_UOM { get; set; }

        [JsonProperty("UOMName")]
        public string UOM_Name { get; set; }
    }
}