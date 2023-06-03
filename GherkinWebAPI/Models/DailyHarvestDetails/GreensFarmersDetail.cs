using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.DailyHarvestDetails
{
    public class GreensFarmersDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Greens_Farmers_Entry_No")]
        [JsonProperty("greensFarmersEntryNo")]
        public int GreensFarmersEntryNo { get; set; }
        [Column("Greens_Procurement_No")]
        [JsonProperty("greensProcurementNo")]
        public int GreensProcurementNo { get; set; }
        [Column("Farmer_Code")]
        [JsonProperty("farmerCode")]
        public string FarmerCode { get; set; }
        [Column("Crop_Scheme_Code")]
        [JsonProperty("cropSchemeCode")]
        public string CropSchemeCode { get; set; }
        [Column("Count_wise_Total_Crates")]
        [JsonProperty("countwiseTotalCrates")]
        public int CountwiseTotalCrates { get; set; }
        [Column("Count_wise_Total_Quantity")]
        [JsonProperty("countwiseTotalQuantity")]
        public decimal CountwiseTotalQuantity { get; set; }
        [Column("Last_Harvest_Status")]
        [JsonProperty("lastHarvestStatus")]
        public string LastHarvestStatus { get; set; }
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