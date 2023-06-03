using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.TransportVehicleManagement
{
	public class HiredVehicleDetail
	{
        [Key]
        [Column("Hired_Vehicle_ID")]
        [JsonProperty("hiredVehicleID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int HiredVehicleID { get; set; }

        [Column("Hired_Trans_ID")]
        [JsonProperty("hiredTransID")]
        [Required]
        public int HiredTransID { get; set; }

        [Column("Vehicle_Type")]
        [JsonProperty("vehicleType")]
        [MaxLength(15)]
        [Required]
        public string VehicleType { get; set; }

        [Column("Vehicle_Make")]
        [JsonProperty("vehicleMake")]
        [MaxLength(30)]
        [Required]
        public string VehicleMake { get; set; }

        [Column("Vehicle_DOP")]
        [JsonProperty("vehicleDOP")]
        [Required]
        public DateTime VehicleDOP { get; set; }

        [Column("Vehicle_Reg_Number")]
        [JsonProperty("vehicleRegNumber")]
        [MaxLength(20)]
        [Required]
        public string VehicleRegNumber { get; set; }

        [Column("Vehicle_Chassis_No")]
        [JsonProperty("vehicleChassisNo")]
        [MaxLength(30)]
        [Required]
        public string VehicleChassisNo { get; set; }

        [Column("Vehicle_Nos_Tyres")]
        [JsonProperty("vehicleNosTyres")]
        [Required]
        public int VehicleNosTyres { get; set; }

        [Column("Vehicle_Avg_Mileage")]
        [JsonProperty("vehicleAvgMileage")]
        [Required]
        public decimal VehicleAvgMileage { get; set; }

        [Column("Vehicle_Renewal_Duration")]
        [JsonProperty("vehicleRenewalDuration")]
        [Required]
        public int VehicleRenewalDuration { get; set; }

        [Column("Vehicle_Max_Capacity")]
        [JsonProperty("vehicleMaxCapacity")]
        [Required]
        public int VehicleMaxCapacity { get; set; }
    }
}