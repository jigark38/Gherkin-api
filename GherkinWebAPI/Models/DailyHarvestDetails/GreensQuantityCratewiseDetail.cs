using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.DailyHarvestDetails
{
    public class GreensQuantityCratewiseDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Greens_Cratewise_Entry_No")]
        [JsonProperty("greensCratewiseEntryNo")]
        public int GreensCratewiseEntryNo { get; set; }
        [Column("Greens_Procurement_No")]
        [JsonProperty("greensProcurementNo")]
        public int GreensProcurementNo { get; set; }
        [Column("Farmer_Code")]
        [JsonProperty("farmerCode")]
        public string FarmerCode { get; set; }
        [Column("Crop_Scheme_Code")]
        [JsonProperty("cropSchemeCode")]
        public string CropSchemeCode { get; set; }
        [Column("No_of_Crates")]
        [JsonProperty("noOfCrates")]
        public int NoofCrates { get; set; }
        [Column("Each_Crate_Wt")]
        [JsonProperty("eachCrateWt")]
        public decimal EachCrateWt { get; set; }
        [Column("Crate_No_From")]
        [JsonProperty("crateNoFrom")]
        public int? CrateNoFrom { get; set; }
        [Column("Crate_No_To")]
        [JsonProperty("crateNoTo")]
        public int? CrateNoTo { get; set; }
        [Column("Gross_Weight")]
        [JsonProperty("grossWeight")]
        public decimal GrossWeight { get; set; }
        [Column("Tare_weight")]
        [JsonProperty("tareweight")]
        public decimal Tareweight { get; set; }
        [Column("Crateswise_Net_Weight")]
        [JsonProperty("crateswiseNetWeight")]
        public decimal CrateswiseNetWeight { get; set; }
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