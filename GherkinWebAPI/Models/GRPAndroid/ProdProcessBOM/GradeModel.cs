using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.GRPAndroid.ProdProcessBOM
{
    public class GradeModel
    {
        [JsonProperty("gradeCode")]
        public string GradeCode { get; set; }



        [JsonProperty("gradeFrom")]
        public int GradeFrom { get; set; }


        [JsonProperty("gradeTo")]
        public int GradeTo { get; set; }
    }
}