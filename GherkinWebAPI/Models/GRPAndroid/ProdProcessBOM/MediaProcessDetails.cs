using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.GRPAndroid.ProdProcessBOM
{
    [Table("Media_Process_Details")]
    public class MediaProcessDetails
    {
        [Key]
        [JsonProperty("mediaProcessCode")]
        [Column("Media_Process_Code")]
        public string MediaProcessCode { get; set; }

        [JsonProperty("productionProcessCode")]
        [Column("Production_Process_Code")]
        public string ProductionProcessCode { get; set; }

        [JsonProperty("mediaProcessName")]
        [Column("Media_Process_Name")]
        public string MediaProcessName { get; set; }

        [JsonProperty("mediaProcessDescription")]
        [Column("Media_Process_Description")]
        public string MediaProcessDescription { get; set; }
    }
}