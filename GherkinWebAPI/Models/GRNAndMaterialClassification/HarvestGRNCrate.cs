using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace GherkinWebAPI.Models.GRNAndMaterialClassification
{
    public class HarvestGRNCrate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Harvest_GRN_Crates_Entry_No")]
        public long HarvestGRNCratesEntryNo { get; set; }
        [Column("Harvest_GRN_No")]
        public long HarvestGRNNo { get; set; }
        [Column("Crop_Scheme_Code")]
        public string CropSchemeCode { get; set; }
        [Column("Crop_Name_Code")]
        public string CropNameCode { get; set; }
        [Column("Harvest_GRN_Crates_Despatch_Nos")]
        public int HarvestGRNCratesDespatchNos { get; set; }
        [Column("Harvest_GRN_Total_Approx_Qty")]
        public decimal HarvestGRNTotalApproxQty { get; set; }
    }
}