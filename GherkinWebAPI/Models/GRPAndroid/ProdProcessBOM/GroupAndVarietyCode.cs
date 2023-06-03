using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.GRPAndroid.ProdProcessBOM
{
    public class GroupAndVarietyCode
    {
        [JsonProperty("fpGroupCode")]

        public string FPGroupCode { get; set; }

        [JsonProperty("fpVarietyCode")]

        public string FPVarietyCode { get; set; }
    }
}