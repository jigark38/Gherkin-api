using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.MediaBatchDetails
{
    public class SelectedPendingOrder
    {
        [JsonProperty("orgOfficeNo")]
        public int OrgOfficeNo { get; set; }
        [JsonProperty("mediaProcessCode")]
        public string MediaProcessCode { get; set; }
        [JsonProperty("pendingOrderObj")]
        public PendingOrderScheduleGrid PendingOrderObj { get; set; }
    }
}