using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.HarvestDeatils
{
    /// <summary>
    /// Defines the <see cref="HarvestGradewiseDetails" />.
    /// </summary>
    [Table("Harvest_Quantity_Gradewise_Details")]
    public class HarvestGradewiseDetails
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        [Column("ID")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the HarvestQuantityEntryNo.
        /// </summary>
        [Column("Harvest_Quantity_Entry_No")]
        public long HarvestQuantityEntryNo { get; set; }

        /// <summary>
        /// Gets or sets the HarvestProcurementNo.
        /// </summary>
        [Column("Harvest_Procurement_No")]
        public long HarvestProcurementNo { get; set; }

        /// <summary>
        /// Gets or sets the CropSchemeCode.
        /// </summary>
        [Column("Crop_Scheme_Code")]
        public string CropSchemeCode { get; set; }

        /// <summary>
        /// Gets or sets the NoOfCrates.
        /// </summary>
        [Column("Total_No_of_Crates")]
        public int NoOfCrates { get; set; }

        /// <summary>
        /// Gets or sets the FarmerHarvestQuantity.
        /// </summary>
        [Column("Total_Farmer_Harvest_Quantity")]
        public decimal FarmerHarvestQuantity { get; set; }
    }
}
