using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace GherkinWebAPI.Models.GreensTransportVehicleSchedules
{
    public class GreensTransportVehicleSchedule
    {
        [Key]
        [JsonProperty("greensTransVehicleDespNo")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Greens_Trans_Vehicle_Desp_No")]
        public int GreensTransVehicleDespNo { get; set; }
        [JsonProperty("entryDate")]
        [Column("Entry_Date")]
        public DateTime EntryDate { get; set; }
        [JsonProperty("enteredEmpId")]
        [Column("Entered_Emp_ID")]
        public string EnteredEmpID { get; set; }
        [JsonProperty("orgOfficeNo")]
        [Column("Org_office_No")]
        public int OrgofficeNo { get; set; }
        [JsonProperty("areaId")]
        [Column("Area_ID")]
        public string AreaId { get; set; }
        [JsonProperty("buyerEmpId")]
        [Column("Buyer_Emp_ID")]
        public string BuyerEmpId { get; set; }
        [JsonProperty("rgpNo")]
        [Column("RGP_No")]
        public string RGPNo { get; set; }
        [JsonProperty("gcTransporterName")]
        [Column("GC_Transporter_Name")]
        public string GCTransporterName { get; set; }
        [JsonProperty("hiredTransId")]
        [Column("Hired_Trans_ID")]
        public int? HiredTransID { get; set; }
        [JsonProperty("ownVehicleID")]
        [Column("Own_Vehicle_ID")]
        public int? OwnVehicleID { get; set; }
        [JsonProperty("hiredVehicleID")]
        [Column("Hired_Vehicle_ID")]
        public int? HiredVehicleID { get; set; }
        [JsonProperty("driverID")]
        [Column("Driver_ID")]
        public int? DriverID { get; set; }
        [JsonProperty("gcDriverName")]
        [Column("GC_Driver_Name")]
        public string GCDriverName { get; set; }
        [JsonProperty("gcDriverContactNo")]
        [Column("GC_Driver_Contact_No")]
        public string GCDriverContactNo { get; set; }
        [JsonProperty("timeofDespatch")]
        [Column("Time_of_Despatch")]
        public TimeSpan TimeofDespatch { get; set; }
        [JsonProperty("vehicleReading")]
        [Column("Vehicle_Reading")]
        public string VehicleReading { get; set; }
        [JsonProperty("rgpRemarks")]
        [Column("RGP_Remarks")]
        public string RGPRemarks { get; set; }

        // Navigation Property
        public virtual ICollection<GreensTransportMaterialDetail> GreensTransportMaterialDetails { get; set; }

    }
}