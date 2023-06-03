using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.GRPAndroid.ProdProcessBOM
{
    public class VarietyModel
    {
        [JsonProperty("varietyCode")]

        public string VarietyCode { get; set; }
        [JsonProperty("varietyName")]
        public string VarietyName { get; set; }
    }
}