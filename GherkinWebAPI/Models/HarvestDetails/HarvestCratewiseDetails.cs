using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.HarvestDeatils
{
    /// <summary>
    /// Defines the <see cref="HarvestCratewiseDetails" />.
    /// </summary>
    [Table("Harvest_Quantity_Cratewise_Details")]
    public class HarvestCratewiseDetails
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        [Column("ID")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the HarvestCratewiseEntryNo.
        /// </summary>
        [Column("Harvest_Cratewise_Entry_No")]
        public long HarvestCratewiseEntryNo { get; set; }

        /// <summary>
        /// Gets or sets the HarvestProcurementNo.
        /// </summary>
        [Column("Harvest_Procurement_No")]
        public long HarvestProcurementNo { get; set; }

        /// <summary>
        /// Gets or sets the HarvestFarmersEntryNo.
        /// </summary>
        [Column("Farmer_Code")]
        public string FarmerCode { get; set; }

        /// <summary>
        /// Gets or sets the CropSchemeCode.
        /// </summary>
        [Column("Crop_Scheme_Code")]
        public string CropSchemeCode { get; set; }

        /// <summary>
        /// Gets or sets the NoOfCrates.
        /// </summary>
        [Column("No_of_Crates")]
        public int NoOfCrates { get; set; }

        /// <summary>
        /// Gets or sets the GrossWeight.
        /// </summary>
        [Column("Gross_Weight")]
        public decimal GrossWeight { get; set; }

        /// <summary>
        /// Gets or sets the Tareweight.
        /// </summary>
        [Column("Tare_weight")]
        public decimal Tareweight { get; set; }

        /// <summary>
        /// Gets or sets the CrateswiseNetWeight.
        /// </summary>
        [Column("Crateswise_Net_Weight")]
        public decimal CrateswiseNetWeight { get; set; }
    }
}
