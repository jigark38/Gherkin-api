using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.GRNAndMaterialClassification
{
    public class HarvestGRN
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Harvest_GRN_No")]
        public long HarvestGRNNo { get; set; }
        [Column("Area_ID")]
        public string AreaID { get; set; }
        [Column("Harvest_GRN_Date")]
        public DateTime HarvestGRNDate { get; set; }
        [Column("Vehicle_No")]
        public string VehicleNo { get; set; }
        [Column("Driver_Name")]
        public string DriverName { get; set; }
        [Column("Driver_Contact_No")]
        public long? DriverContactNo { get; set; }
        [Column("Vehicle_Starting_Reading")]
        public int VehicleStartingReading { get; set; }
        [Column("Vehicle_Start_time")]
        public DateTime VehicleStartTime { get; set; }
        [Column("Vehicle_Freight")]
        public int VehicleFreight { get; set; }
        [Column("Harvest_GRN_Total_Quantity")]
        public decimal HarvestGRNTotalQuantity { get; set; }
        [Column("Harvest_GRN_Total_Desp_Crates")]
        public int HarvestGRNTotalDespCrates { get; set; }
        [Column("Harverst_GRN_Remarks")]
        public string HarverstGRNRemarks { get; set; }
        [Column("Org_Office_No")]
        public int OrgOfficeNo { get; set; }
        [Column("Employee_ID")]
        public string EmployeeId { get; set; }
        [Column("Loading_Completed")]
        public int LoadingCompleted { get; set; }
        [Column("Greens_Trans_Vehicle_Desp_No")]
        public int? GreensTransVehicleDespNo { get; set; }
    }
}