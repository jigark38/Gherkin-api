using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.GRPAndroid.ProdProcessBOM
{
    public class RawMaterialGroupModel
    {
        [JsonProperty("rawMaterialGroupCode")]
        public string RawMaterialGroupCode { get; set; }
        [JsonProperty("rawMaterialGroup")]
        public string RawMaterialGroup { get; set; }
    }
}