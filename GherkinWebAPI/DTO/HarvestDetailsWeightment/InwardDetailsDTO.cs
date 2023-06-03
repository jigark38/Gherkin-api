using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO
{
    public class InwardDetailsDTO
    {
        [JsonProperty("inwardType")]
        public string InWardType { get; set; }
        [JsonProperty("igpDate")]
        public DateTime InwardDate { get; set; }
        [JsonProperty("igpNo")]
        public string InwardGatePassNo { get; set; }
        [JsonProperty("supplierTransporter")]
        public string SupplierTransporter { get; set; }
        [JsonProperty("veichleNo")]
        public string VeichleNo { get; set; }
    }
}