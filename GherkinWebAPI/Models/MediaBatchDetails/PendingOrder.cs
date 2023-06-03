using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.MediaBatchDetails
{
    public class PendingOrder
    {
        [JsonProperty("orgOfficeNo")]
        public int OrgOfficeNo { get; set; }
        [JsonProperty("mediaProcessCode")]
        public string MediaProcessCode { get; set; }
        [JsonProperty("mediaBatchProductionNo")]
        public long MediaBatchProductionNo { get; set; }
        [JsonProperty("MediaBatchProductionVisibleNo")]
        public string MediaBatchProductionVisibleNo { get; set; }
        [JsonProperty("pendingOrderObj")]
        public PendingOrderScheduleGrid PendingOrderObj { get; set; }
    }
}