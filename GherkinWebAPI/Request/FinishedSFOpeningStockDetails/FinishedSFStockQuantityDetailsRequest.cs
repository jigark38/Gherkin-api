using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Request.FinishedSFOpeningStockDetails
{
    public class FinishedSFStockQuantityDetailsRequest
    {
        [JsonProperty("fSFStockQuantityNo")]
        public int FSFStockQuantityNo { get; set; }
        [JsonProperty("fSFStockProcessedDate")]
        public DateTime FSFStockProcessedDate { get; set; }
        [JsonProperty("fSFOSStockNo")]
        public int FSFOSStockNo { get; set; }
        [JsonProperty("containerCode")]
        public int ContainerCode { get; set; }
        [JsonProperty("containerName")]
        public string ContainerName { get; set; }
        [JsonProperty("quantityContainer")]
        public decimal QuantityContainer { get; set; }
        [JsonProperty("gSCUOMCode")]
        public int GSCUOMCode { get; set; }
        [JsonProperty("gSCUOMName")]
        public string GSCUOMName { get; set; }
        [JsonProperty("containerWeight")]
        public decimal ContainerWeight { get; set; }
        [JsonProperty("fSFNOofContainers")]
        public int FSFNOofContainers { get; set; }
        [JsonProperty("containerSlNoFrom")]
        public string ContainerSlNoFrom { get; set; }
        [JsonProperty("containerSlNoTo")]
        public string ContainerSlNoTo { get; set; }
        [JsonProperty("stockLocationDetails")]
        public string StockLocationDetails { get; set; }
        [JsonProperty("barcodeOption")]
        public string BarcodeOption { get; set; }
    }
}