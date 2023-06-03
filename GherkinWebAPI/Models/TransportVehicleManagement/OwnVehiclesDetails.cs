using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.TransportVehicleManagement
{
    public class OwnVehiclesDetails
    {
        [Required]
        [Column("Emp_Created_ID")]
        [MaxLength(20)]
        [JsonProperty("empCreatedId")]
        public string EmpCreatedID { get; set; }
        [Required]
        [Column("Vehicle_Entry_Date")]
        [JsonProperty("vehicleEntryDate")]
        public DateTime VehicleEntryDate { get; set; }
        [Required]
        [Column("Vehicle_Type")]
        [JsonProperty("vehicleType")]
        [MaxLength(15)]
        public string VehicleType { get; set; }
        [Required]
        [Column("Vehicle_Make")]
        [JsonProperty("vehicleMake")]
        [MaxLength(30)]
        public string VehicleMake { get; set; }
        [Required]
        [Column("Vehicle_DOP")]
        [JsonProperty("vehicleDOP")]
        public DateTime VehicleDOP { get; set; }
        [Key]
        [Column("Own_Vehicle_ID")]
        [JsonProperty("ownVehicleId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OwnVehicleID { get; set; }
        [Required]
        [Column("Vehicle_Reg_Number")]
        [JsonProperty("vehicleRegNumber")]
        [MaxLength(20)]
        public string VehicleRegNumber { get; set; }
        [Required]
        [Column("Vehicle_Chassis_No")]
        [JsonProperty("vehicleChassisNo")]
        [MaxLength(30)]
        public string VehicleChassisNo { get; set; }
        [Required]
        [Column("Vehicle_Nos_Tyres")]
        [JsonProperty("vehicleNosTyres")]
        public int VehicleNosTyres { get; set; }
        [Required]
        [Column("Vehicle_Avg_Mileage")]
        [JsonProperty("vehicleAvgMileage")]
        public decimal VehicleAvgMileage { get; set; }
        [Required]
        [Column("Vehicle_Renewal_Duration")]
        [JsonProperty("vehicleRenewalDuration")]
        public int VehicleRenewalDuration { get; set; }
        [Required]
        [Column("Vehicle_Max_Capacity")]
        [JsonProperty("vehicleMaxCapacity")]
        public int VehicleMaxCapacity { get; set; }

        public ICollection<OwnVehicleDocuments> OwnVehicleDocuments { get; set; }
    }

}