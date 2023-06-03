using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.StoresMasterDetails
{
    public class GSMaterialDetail
	{
        [Key]
        [Column("GS_Material_Code")]
        [JsonProperty("gsMaterialCode")]
        [Required]
        public int GSMaterialCode { get; set; }


        [Column("GS_Entry_Date")]
        [JsonProperty("gsEntryDate")]
        [Required]
        public DateTime GSEntryDate { get; set; }

        [Column("Employee_ID")]
        [JsonProperty("employeeID")]
        [Required]
        public string EmployeeID { get; set; }

        [Column("GS_Group_Code")]
        [JsonProperty("gsGroupCode")]
        [Required]
        public int GSGroupCode { get; set; }

        [Column("GS_Sub_Group_Code")]
        [JsonProperty("gsSubGroupCode")]
        [Required]
        public int GSSubGroupCode { get; set; }

        [Column("GS_Material_Name")]
        [JsonProperty("gsMaterialName")]
        [MaxLength(100)]
        [Required]
        public string GSMaterialName { get; set; }

        [Column("GS_Material_Desc")]
        [JsonProperty("gsMaterialDesc")]
        [MaxLength(300)]
        [Required]
        public string GSMaterialDesc { get; set; }

        [Column("GSC_UOM_Code")]
        [JsonProperty("gscUOMCode")]
        public int GSCUOMCode { get; set; }

        [Column("Packing_Size_Unit")]
        [JsonProperty("packingSizeUnit")]
        public int? PackingSizeUnit { get; set; }

        [Column("Qty_per_Pack_Unit")]
        [JsonProperty("qtyPerPackUnit")]
        public int? QtyPerPackUnit { get; set; }

        [Column("Location")]
        [JsonProperty("location")]
        [MaxLength(30)]
        public string Location { get; set; }

        [Column("ROL_Quantity")]
        [JsonProperty("rolQuantity")]
        public decimal? ROLQuantity { get; set; }

        [Column("HSN_Code")]
        [JsonProperty("hsnCode")]
        public int? HSNCode { get; set; }

        [Column("IGST_Rate")]
        [JsonProperty("igstRate")]
        public decimal? IGSTRate { get; set; }

        [Column("CGST_Rate")]
        [JsonProperty("cgstRate")]
        public decimal? CGSTRate { get; set; }

        [Column("SGST_Rate")]
        [JsonProperty("sgstRate")]
        public decimal? SGSTRate { get; set; }

        [Column("Opening_Stock")]
        [JsonProperty("openingStock")]
        public DateTime? OpeningStock { get; set; }

        [Column("No_of_Package_Units")]
        [JsonProperty("noOfPackageUnits")]
        public int? NoOfPackageUnits { get; set; }

        [Column("Opening_Stock_Quantity")]
        [JsonProperty("openingStockQuantity")]
        public decimal? OpeningStockQuantity { get; set; }

        [Column("Rate_Rate")]
        [JsonProperty("rateRate")]
        public decimal? RateRate { get; set; }

        [Column("Opening_Stock_Value")]
        [JsonProperty("openingStockValue")]
        public decimal? OpeningStockValue { get; set; }

        [NotMapped]
        [JsonProperty("gsGroupName")]
        public string GSGroupName { get; set; }

        [NotMapped]
        [JsonProperty("gsSubGroupName")]
        public string GSSubGroupName { get; set; }

        [NotMapped]
        [JsonProperty("gscUOMName")]
        public string GSCUOMName { get; set; }

    }
}