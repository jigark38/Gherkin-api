using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.HarvestDeatils
{
    /// <summary>
    /// Defines the <see cref="HarvestFarmersDetails" />.
    /// </summary>
    [Table("Harvest_Farmers_Details")]
    public class HarvestFarmersDetails
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        [Column("ID")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the HarvestFarmersEntryNo.
        /// </summary>
        [Column("Harvest_Farmers_Entry_No")]
        public long HarvestFarmersEntryNo { get; set; }

        /// <summary>
        /// Gets or sets the HarvestProcurementNo.
        /// </summary>
        [Column("Harvest_Procurement_No")]
        public long HarvestProcurementNo { get; set; }

        /// <summary>
        /// Gets or sets the FarmerCode.
        /// </summary>
        [Column("Farmer_Code")]
        public string FarmerCode { get; set; }

        /// <summary>
        /// Gets or sets the FarmerwiseTotalCrates.
        /// </summary>
        [Column("Farmer_wise_Total_Crates")]
        public int FarmerwiseTotalCrates { get; set; }

        /// <summary>
        /// Gets or sets the FarmerwiseTotalQuantity.
        /// </summary>
        [Column("Farmer_wise_Total_Quantity")]
        public decimal FarmerwiseTotalQuantity { get; set; }

        /// <summary>
        /// Gets or sets the LastHarvestStatus.
        /// </summary>
        [Column("Last_Harvest_Status")]
        public string LastHarvestStatus { get; set; }

        /// <summary>
        /// Gets or sets the CropSchemeCode.
        /// </summary>
        [Column("Crop_Scheme_Code")]
        public string CropSchemeCode { get; set; }


        [NotMapped]
        public List<HarvestCratewiseDetails> HarvestQuantityCratewiseDetails { get; set; }

       
    }
}
