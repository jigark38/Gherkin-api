using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.PackageOfPracticeModel
{
    [Table("HBOM_Package_Practice_Materials")]
    public class PackagePracticeMaterials
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("practiceNo")]
        [Column("HBOM_Practice_NO")]
        [Required]
        [MaxLength(10)]
        public string PracticeNo { get; set; }

        [Column("Crop_Name_Code")]
        [Required]
        [MaxLength(10)]
        [JsonProperty("cropnameCode")]
        public string CropNameCode { get; set; }

        [Required]
        [Column("HBOM_Days_Applicable")]
        [JsonProperty("daysApplicable")]
        public int DaysApplicable { get; set; }

        [Column("HBOM_Trade_Name")]
        [Required]
        [MaxLength(50)]
        [JsonProperty("tradeName")]
        public string TradeName { get; set; }
      
        [Column("HBOM_Chemical_Volume")]
        [Required]
        [JsonProperty("chemicalvolume")]
        [Range(typeof(Decimal), "0", "9999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        public decimal Chemicalvolume { get; set; }
        [Column("HBOM_Chemical_UOM")]
        [Required]
        [JsonProperty("chemicalUOM")]
        public string ChemicalUOM { get; set; }

        [Column("HBOM_Spray_Volume")]
        [Required]
        [JsonProperty("sprayvolume")]
        public int Sprayvolume { get; set; }

        [Column("HBOM_Chemical_Qty")]
        [Required]
        [JsonProperty("chemicalQty")]
        public int ChemicalQty { get; set; }

        [Column("HBOM_Target_Pest")]
        [Required]
        [MaxLength(80)]
        [JsonProperty("tragetPest")]
        public string TragetPest { get; set; }

        [Required]
        [MaxLength(10)]
        [Column("Raw_Material_Group_Code")]
        [JsonProperty("rawmaterialGroupcode")]
        public string RawmaterialGroupcode { get; set; }

        [Required]
        [MaxLength(10)]
        [Column("Raw_Material_Details_Code")]
        [JsonProperty("rawmaterialDetailscode")]
        public string Raw_Material_Details_Code { get; set; }


        [Column("CreatedDate")]
        [JsonProperty("createdDate")]
        public Nullable<DateTime> CreatedDate { get; set; }

        [Column("ModifyDate")]
        [JsonProperty("modifyDate")]
        public Nullable<DateTime> ModifyDate { get; set; }
    }
}