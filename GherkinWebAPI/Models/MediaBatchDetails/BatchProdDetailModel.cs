using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.MediaBatchDetails
{
    public class BatchProdDetailModel
    {
        [JsonProperty("batchProductionNo")]
        public long BatchProductionNo { get; set; }


        [JsonProperty("mediaProcessCode")]
        public string MediaProcessCode { get; set; }

        [JsonProperty("batchProductionDate")]
        public DateTime BatchProductionDate { get; set; }
    }
}