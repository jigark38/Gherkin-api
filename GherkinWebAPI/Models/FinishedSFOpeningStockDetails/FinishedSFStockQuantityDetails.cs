using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class FinishedSFStockQuantityDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("FSF_Stock_Quantity_No")]
        [JsonProperty("fSFStockQuantityNo")]
        public int FSFStockQuantityNo { get; set; }
        [Column("FSF_Stock_Processed_Date")]
        [JsonProperty("fSFStockProcessedDate")]
        public DateTime FSFStockProcessedDate { get; set; }
        [Column("FSF_OS_Stock_No")]
        [JsonProperty("fSFOSStockNo")]
        public int FSFOSStockNo { get; set; }
        [Column("Container_Code")]
        [JsonProperty("containerCode")]
        public int ContainerCode { get; set; }
        [Column("Quantity_Container")]
        [JsonProperty("quantityContainer")]
        public decimal QuantityContainer { get; set; }
        [Column("GSC_UOM_Code")]
        [JsonProperty("gSCUOMCode")]
        public int GSCUOMCode { get; set; }
        [Column("Container_Weight")]
        [JsonProperty("containerWeight")]
        public decimal ContainerWeight { get; set; }
        [Column("FSF_NO_of_Containers")]
        [JsonProperty("fSFNOofContainers")]
        public int FSFNOofContainers { get; set; }
        [Column("Container_Sl_No_From")]
        [JsonProperty("containerSlNoFrom")]
        public string ContainerSlNoFrom { get; set; }
        [Column("Container_Sl_No_To")]
        [JsonProperty("containerSlNoTo")]
        public string ContainerSlNoTo { get; set; }
        [Column("Stock_Location_Details")]
        [JsonProperty("stockLocationDetails")]
        public string StockLocationDetails { get; set; }
        [Column("Barcode_Option")]
        [JsonProperty("barcodeOption")]
        public string BarcodeOption { get; set; }
        
    }
}