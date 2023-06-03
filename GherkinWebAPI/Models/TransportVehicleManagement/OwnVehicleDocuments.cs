using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.TransportVehicleManagement
{
    public class OwnVehicleDocuments
    {
        [Key]
        [Column("Doc_Upload_No")]
        [JsonProperty("docUploadNo")]
        [MaxLength(15)]
        [Required]
        public string DocUploadNo { get; set; }
        [Column("Document_Name")]
        [JsonProperty("documentName")]
        [MaxLength(30)]
        public string DocumentName { get; set; }
        [Column("Own_Vehicle_ID")]
        [JsonProperty("ownVehicleID")]
        public int? OwnVehicleID { get; set; }
        [Column("Document_Details")]
        [JsonProperty("documentDetails")]
        [MaxLength]
        public byte[] DocumentDetails { get; set; }

        [ForeignKey("OwnVehicleID")]
        public OwnVehiclesDetails OwnVehiclesDetail { get; set; }
    }
}