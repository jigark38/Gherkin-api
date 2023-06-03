using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class CropFromTo
    {
        [JsonProperty("psNumber")]
        public string PSNumber { get; set; }
        [JsonProperty("seasonFrom")]
        public DateTime SeasonFrom { get; set; }
        [JsonProperty("seasonTo")]
        public DateTime SeasonTo { get; set; }
        [JsonProperty("cropNameCode")]
        public string CropNameCode { get; set; }
        [JsonProperty("cropGroupCode")]
        public string CropGroupCode { get; set; }
    }
}