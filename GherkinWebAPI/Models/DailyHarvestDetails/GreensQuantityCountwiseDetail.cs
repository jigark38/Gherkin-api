using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.DailyHarvestDetails
{
    public class GreensQuantityCountwiseDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Greens_Quantity_Entry_No")]
        [JsonProperty("greensQuantityEntryNo")]
        public int GreensQuantityEntryNo { get; set; }
        [Column("Greens_Procurement_No")]
        [JsonProperty("greensProcurementNo")]
        public int GreensProcurementNo { get; set; }
        [Column("Crop_Scheme_Code")]
        [JsonProperty("cropSchemeCode")]
        public string CropSchemeCode { get; set; }
        [Column("Total_No_of_Crates")]
        [JsonProperty("totalNoOfCrates")]
        public int TotalNoOfCrates { get; set; }
        [Column("Total_Farmer_Harvest_Quantity")]
        [JsonProperty("totalFarmerHarvestQuantity")]
        public decimal TotalFarmerHarvestQuantity { get; set; }
        [Column("Crop_Group_Code")]
        [JsonProperty("cropGroupCode")]
        public string CropGroupCode { get; set; }
        [Column("Crop_Name_Code")]
        [JsonProperty("cropNameCode")]
        public string CropNameCode { get; set; }
        [Column("PS_Number")]
        [JsonProperty("pSNumber")]
        public string PSNumber { get; set; }
    }
}