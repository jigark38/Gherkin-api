using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class FarmerDocument
    {
        [Key]
        [Column("Farmer_Document_ID")]
        [JsonProperty("id")]
        public int ID { get; set; }
        [Required]
        [MaxLength(7)]
        [JsonProperty("farmerCode")]
        public string Farmer_Code { get; set; }

        [Column("Farmer_Document_Name")]
        [JsonProperty("documentName")]
        public string DocumentName { get; set; }
        [Column("Supplier_Document_Details")]
        [JsonProperty("farmerDocument")]
        public byte[] Document { get; set; }

    }
}