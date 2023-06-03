using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.DailyInputAndFeedingDetails
{
    public class InputIssuesGridModel
    {
        [JsonProperty("popDivision")]
        public string POPDivision { get; set; }
        [JsonProperty("groupMaterialName")]
        public string GroupMaterialName { get; set; }
        [JsonProperty("tradeName")]
        public string TradeName { get; set; }
        [JsonProperty("popStdPerUOM")]
        public string POPStdPerUOM { get; set; }
        [JsonProperty("qtyReqAcre")]
        public decimal QtyReqAcre { get; set; }
        [JsonProperty("transferedTillDate")]
        public decimal TransferedTillDate { get; set; }
        [JsonProperty("toBeIssueQuantity")]
        public decimal ToBeIssueQuantity { get; set; }
        [JsonProperty("transferQty")]
        public decimal TransferQty { get; set; }
        [JsonProperty("rawMaterialGroupCode")]
        public string RawMaterialGroupCode { get; set; }
        [JsonProperty("rawMaterialDetailCode")]
        public string RawMaterialDetailCode { get; set; }
    }
}