using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.TransportVehicleManagement
{
    public class GPSTrackingDevices
    {
        [Key]
        [Column("GPS_Tracking_Device_ID")]
        [JsonProperty("gpsTrackingDeviceId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GPSTrackingDeviceID { get; set; }
        [Column("Own_Vehicle_ID")]
        [JsonProperty("ownVehicleID")]
        public int? OwnVehicleID { get; set; }
        [Column("Hired_Vehicle_ID")]
        [JsonProperty("hiredVehicleID")]
        public int? HiredVehicleID { get; set; }
        [Column("GPS_Device_No")]
        [JsonProperty("gpsDeviceNo")]
        [MaxLength(30)]
        public string GPSDeviceNo { get; set; }
    }

}