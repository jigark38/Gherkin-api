using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.GRPAndroid.ProdProcessBOM
{
    public class ProdGroupCode
    {
        [JsonProperty("groupCode")]
        public string GroupCode { get; set; }
        [JsonProperty("grpName")]
        public string GrpName { get; set; }
    }
}