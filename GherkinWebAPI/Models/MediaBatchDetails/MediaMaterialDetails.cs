using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.MediaBatchDetails
{
    public class MediaMaterialDetails
    {
        [JsonProperty("materialName")]
        public string MaterialName { get; set; }
        [JsonProperty("standardQty")]
        public string StandardQty { get; set; }
        [JsonProperty("requiredQty")]
        public decimal RequiredQty { get; set; }
        [JsonProperty("materialBatchNo")]
        public string MaterialBatchNo { get; set; }
        [JsonProperty("consumedQty")]
        public int ConsumedQty { get; set; }
    }
}