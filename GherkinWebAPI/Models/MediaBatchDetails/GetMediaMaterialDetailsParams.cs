using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.MediaBatchDetails
{
    public class GetMediaMaterialDetailsParams
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }
        [JsonProperty("mediaProcessCode")]
        public string MediaProcessCode { get; set; }
        [JsonProperty("totalQty")]
        public decimal TotalQty { get; set; }
    }
}