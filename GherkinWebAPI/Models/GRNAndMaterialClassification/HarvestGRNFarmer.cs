using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.GRNAndMaterialClassification
{
    public class HarvestGRNFarmer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Harvest_GRN_Farmer_Entry_No")]
        public long HarvestGRNFarmerEntryNo { get; set; }
        [Column("Harvest_GRN_No")]
        public long HarvestGRNNo { get; set; }
        [Column("Greens_Farmers_Entry_No")]
        public int GreensFarmersEntryNo { get; set; }
        [Column("Crop_Name_Code")]
        public string CropNameCode { get; set; }
        [Column("Crop_Scheme_Code")]
        public string CropSchemeCode { get; set; }
        [Column("No_of_Crates")]
        public int NoofCrates { get; set; }
        [Column("Farmer_Wise_Total_Quantity")]
        public decimal FarmerWiseTotalQuantity { get; set; }
    }
}