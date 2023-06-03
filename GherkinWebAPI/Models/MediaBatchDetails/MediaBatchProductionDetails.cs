using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.MediaBatchDetails
{
    [Table("Media_Batch_Production_Details")]
    public class MediaBatchProductionDetails
    {
        [Key]
        [Column("Media_Batch_Production_No")]
        [JsonProperty("mediaBatchProductionNo")]
        public long MediaBatchProductionNo { get; set; }
        [Column("Org_office_No")]
        [JsonProperty("orgofficeNo")]
        public int OrgofficeNo { get; set; }
        [Column("Media_Batch_Production_Date")]
        [JsonProperty("mediaBatchProductionDate")]
        public DateTime MediaBatchProductionDate { get; set; }
        [Column("Media_Batch_Production_Visible_No")]
        [JsonProperty("mediaBatchProductionVisibleNo")]
        public string MediaBatchProductionVisibleNo { get; set; }
        [Column("Employee_ID")]
        [JsonProperty("employeeID")]
        public string EmployeeID { get; set; }
        [Column("Production_Quantity")]
        [JsonProperty("productionQuantity")]
        public decimal ProductionQuantity { get; set; }
        [Column("Media_Batch_Size")]
        [JsonProperty("mediaBatchSize")]
        public int MediaBatchSize { get; set; }
        [Column("Media_Batch_UOM")]
        [JsonProperty("mediaBatchUOM")]
        public string MediaBatchUOM { get; set; }
        [Column("S_NaOH")]
        [JsonProperty("sNaOH")]
        public string SNaOH { get; set; }
        [Column("S_AgNO3")]
        [JsonProperty("sAgNO3")]
        public string SAgNO3 { get; set; }
        [Column("S_BRINE")]
        [JsonProperty("sBRINE")]
        public string SBRINE { get; set; }
        [Column("Water")]
        [JsonProperty("water")]
        public string Water { get; set; }
        [Column("Media_Batch_Remarks")]
        [JsonProperty("mediaBatchRemarks")]
        public string MediaBatchRemarks { get; set; }
    }
}