using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class GroupCode
    {
        [JsonProperty("cropGroupCode")]
        public string CropGroupCode { get; set; }
        [JsonProperty("cropGroupName")]
        public string CropGroupName { get; set; }
    }
}