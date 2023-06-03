using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.MediaBatchDetails
{
    public class BatchSchOrdProdModel
    {
        [JsonProperty("bsOrderProductionNo")]
        public long BSOrderProductionNo { get; set; }

        [JsonProperty("psSalesOrderScheduleNo")]
        public long? PSSalesOrderScheduleNo { get; set; }
        [JsonProperty("psDirectOrderScheduleNo")]
        public long? PSDirectOrderSchedule_No { get; set; }
        [JsonProperty("fpGroupCode")]
        public string FPGroupCode { get; set; }
        [JsonProperty("fpVarietyCode")]
        public string FPVarietyCode { get; set; }
        [JsonProperty("fpGradeCode")]
        public string FPGradeCode { get; set; }
        [JsonProperty("packUOM")]
        public string PackUOM { get; set; }
        [JsonProperty("bsProductionQtyinUOM")]
        public long BSProductionQtyinUOM { get; set; }
    }
}