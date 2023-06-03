using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.Product_GradeDto
{
    public class ProductGroupDto
    {
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }
        [JsonProperty("groupCode")]
        public string GroupCode { get; set; }
        [JsonProperty("grpName")]
        public string GrpName { get; set; }
        [JsonProperty("varietyName")]

        public string VarietyName { get; set; }
        [JsonProperty("scintificName")]

        public string ScintificName { get; set; }
        [JsonProperty("gradefromTo")]

        public string GradeFromTo { get; set; }
      
    }
}