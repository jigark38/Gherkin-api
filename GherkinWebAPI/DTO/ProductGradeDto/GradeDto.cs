using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO
{
    public class GradeDto
    {
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("varietyCode")]
        public string VarietyCode { get; set; }
        [JsonProperty("gradeCode")]
        public string GradeCode { get; set; }
        [JsonProperty("gradeFrom")]
        public int GradeFrom { get; set; }
        [JsonProperty("gradeTo")]
        public int GradeTo { get; set; }
    }
}