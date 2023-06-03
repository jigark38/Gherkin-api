using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.MediaBatchDetails
{
    public class UnitAndMediaFilter
    {
        [JsonProperty("orgOfficeNo")]
        public int OrgOfficeNo { get; set; }
        [JsonProperty("mediaProcessCode")]
        public string MediaProcessCode { get; set; }
    }
}