using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.GRPAndroid.ProdProcessBOM
{
    public class RawMaterialDetailModel
    {
        [JsonProperty("rawMaterialDetailsCode")]
        public string RawMaterialDetailsCode { get; set; }
        [JsonProperty("rawMaterialDetailsName")]
        public string RawMaterialDetailsName { get; set; }
    }
}