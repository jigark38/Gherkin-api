using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace GherkinWebAPI.Models.BatchProduction
{
    public class BatchScheduleDrumsBarcodeDetails
    {
        [JsonProperty("batchProductionNo")]
        public long Batch_Production_No { get; set; }

        [JsonProperty("bSOrderProductionNo")]
        public long? BS_Order_Production_No { get; set; }

        [JsonProperty("mediaProcessCode")]
        public string Media_Process_Code { get; set; }
        [Key]
        [JsonProperty("prodBarrelNo")]
        public Guid Prod_Barrel_No { get; set; }

        [JsonProperty("prodBarrelQRNo")]
        public string Prod_Barrel_QR_No { get; set; }
    }
}