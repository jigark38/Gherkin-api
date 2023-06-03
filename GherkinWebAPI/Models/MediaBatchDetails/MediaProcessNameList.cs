using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.MediaBatchDetails
{
    public class MediaProcessNameList
    {
        [JsonProperty("mediaProcessCode")]
        public string MediaProcessCode { get; set; }

        [JsonProperty("mediaProcessName")]
        public string MediaProcessName { get; set; }
    }
}