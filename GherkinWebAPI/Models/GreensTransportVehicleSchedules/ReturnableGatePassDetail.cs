using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace GherkinWebAPI.Models.GreensTransportVehicleSchedules
{
    public class ReturnableGatePassDetail
    {
        [JsonProperty("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int ID { get; set; }
        [Key]
        [Column("RGP_No")]
        public string RGPNo { get; set; }
        [JsonProperty("rgpDate")]
        [Column("RGP_Date")]
        public DateTime RGPDate { get; set; }
    }
}