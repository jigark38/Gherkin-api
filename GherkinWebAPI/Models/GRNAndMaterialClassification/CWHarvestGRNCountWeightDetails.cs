using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace GherkinWebAPI.Models.GRNAndMaterialClassification
{
    public class CWHarvestGRNCountWeightDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CW_Greens_Cratewise_Entry_No")]
        [JsonProperty("CWGreensCratewiseEntryNo")]
        public int CWGreensCratewiseEntryNo { get; set; }
        [Column("Harvest_GRN_No")]
        [JsonProperty("HarvestGRNNo")]
        public Int64 HarvestGRNNo { get; set; }
        [Column("Crop_Group_Code")]
        [JsonProperty("cropGroupCode")]
        public string CropGroupCode { get; set; }
        [Column("Crop_Name_Code")]
        [JsonProperty("cropNameCode")]
        public string CropNameCode { get; set; }
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
        [Column("CW_Gross_Weight")]
        [JsonProperty("grossWeight")]
        public decimal CWGrossWeight { get; set; }
        [Column("CW_Tare_weight")]
        [JsonProperty("tareweight")]
        public decimal CWTareweight { get; set; }
        [Column("CW_Crateswise_Net_Weight")]
        [JsonProperty("crateswiseNetWeight")]
        public decimal CWCrateswiseNetWeight { get; set; }
        [Column("Greens_Procurement_No")]
        [JsonProperty("greensProcurementNo")]
        public int? GreensProcurementNo { get; set; }
        [Column("Buyer_Employee_ID")]
        [JsonProperty("BuyerEmployeeID")]
        public string BuyerEmployeeID { get; set; }
    }
}