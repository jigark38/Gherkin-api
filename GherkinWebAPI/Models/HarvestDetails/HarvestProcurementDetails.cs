using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.HarvestDeatils
{
    /// <summary>
    /// Defines the <see cref="HarvestProcurementDetails" />.
    /// </summary>
    [Table("Harvest_Procurement_Details")]
    public class HarvestProcurementDetails
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        [Column("ID")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the HarvestProcurementNo.
        /// </summary>
        [Column("Harvest_Procurement_No")]
        public long HarvestProcurementNo { get; set; }

        /// <summary>
        /// Gets or sets the HarvestDate.
        /// </summary>
        [Column("Harvest_Date")]
        public DateTime HarvestDate { get; set; }

        /// <summary>
        /// Gets or sets the AreaId.
        /// </summary>
        [Column("Area_ID")]
        public string AreaId { get; set; }

        /// <summary>
        /// Gets or sets the CropNameCode.
        /// </summary>
        [Column("Crop_Name_Code")]
        public string CropNameCode { get; set; }

        /// <summary>
        /// Gets or sets the PsNumber.
        /// </summary>
        [Column("PS_Number")]
        public string PsNumber { get; set; }

        /// <summary>
        /// Gets or sets the BuyingSupervisorEmployeeId.
        /// </summary>
        [Column("Buying_Supervisor_Employee_ID")]
        public long BuyingSupervisorEmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the BuyingAsstEmployeeId.
        /// </summary>
        [Column("Buying_Asst_Employee_ID")]
        public long BuyingAsstEmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the VehicleNumber.
        /// </summary>
        [Column("Vehicle_Number")]
        public string VehicleNumber { get; set; }

        /// <summary>
        /// Gets or sets the VehicleDriverName.
        /// </summary>
        [Column("Vehicle_Driver_Name")]
        public string VehicleDriverName { get; set; }

        /// <summary>
        /// Gets or sets the HarvestStartingTime.
        /// </summary>
        [Column("Harvest_Starting_Time")]
        public DateTime HarvestStartingTime { get; set; }

        /// <summary>
        /// Gets or sets the HarvestStartingKms.
        /// </summary>
        [Column("Harvest_Starting_KMS")]
        public int HarvestStartingKms { get; set; }

        /// <summary>
        /// Gets or sets the HarvestEndingTime.
        /// </summary>
        [Column("Harvest_Ending_Time")]
        public DateTime HarvestEndingTime { get; set; }

        /// <summary>
        /// Gets or sets the HarvestEndingKMS.
        /// </summary>
        [Column("Harvest_Ending_KMS")]
        public int HarvestEndingKMS { get; set; }

        /// <summary>
        /// Gets or sets the HarvestTimeDuration.
        /// </summary>
        [Column("Harvest_Time_Duration")]
        public decimal HarvestTimeDuration { get; set; }

        /// <summary>
        /// Gets or sets the HarvestKmsTotalReading.
        /// </summary>
        [Column("Harvest_KMS_Total_Reading")]
        public int HarvestKmsTotalReading { get; set; }

        /// <summary>
        /// Gets or sets the VehicleCharges.
        /// </summary>
        [Column("Vehicle_Charges")]
        public int VehicleCharges { get; set; }

        /// <summary>
        /// Gets or sets the HarvestOtherCharges.
        /// </summary>
        [Column("Harvest_Other_Charges")]
        public int HarvestOtherCharges { get; set; }

        /// <summary>
        /// Gets or sets the TripTotalQuantity.
        /// </summary>
        [Column("Trip_Total_Quantity")]
        public decimal TripTotalQuantity { get; set; }

        /// <summary>
        /// Gets or sets the TripTotalCrates.
        /// </summary>
        [Column("Trip_Total_Crates")]
        public int TripTotalCrates { get; set; }

        /// <summary>
        /// Gets or sets the HaverstRemarks.
        /// </summary>
        [Column("Haverst_Remarks")]
        public string HaverstRemarks { get; set; }

        /// <summary>
        /// Gets or sets the EmployeeId.
        /// </summary>
        [Column("Employee_ID")]
        public string EmployeeId { get; set; }

        [NotMapped]
        public List<HarvestFarmersDetails> HarvestFarmersDetails { get; set; }
    }
}
