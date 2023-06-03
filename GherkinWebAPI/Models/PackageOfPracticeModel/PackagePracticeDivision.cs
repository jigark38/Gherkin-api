using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.PackageOfPracticeModel
{
    [Table("HBOM_Package_Practice_Division")]
    public class PackagePracticeDivision
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        [Column("HBOM_Division_For")]
        [JsonProperty("divisionFor")]
        public string DivisionFor { get; set; }
        [Column("Crop_Name_Code")]
        [Required]
        [MaxLength(10)]
        [JsonProperty("cropnameCode")]
        public string CropNameCode { get; set; }

        [Column("HBOM_Practice_Per_Acreage")]
        [Required]
        [MaxLength(25)]
        [JsonProperty("practiceperAcre")]
        public string PracticePerAcre { get; set; }

        [Column("PS_Number")]
        [Required]
        [MaxLength(10)]
        [JsonProperty("psNo")]
        public string PSNO { get; set; }
        [Column("HS_Transaction_Code")]
        [Required]
        [MaxLength(10)]
        [JsonProperty("transCode")]
        public string TransCode { get; set; }
        [Column("HS_Crop_Phase_Code")]
        [Required]
        [MaxLength(10)]
        [JsonProperty("cropphaseCode")]
        public string CropphaseCode { get; set; }

        [Required]        
        [Column("HBOM_Practice_Effective_Date")]
        [JsonProperty("practiceeffectiveDate")]
        public DateTime PracticeEffectiveDate { get; set; }
        [Key]

        [Column("HBOM_Practice_NO")]
        [JsonProperty("practiceNo")]
        public string PracticeNo { get; set; }
        [Column("CreatedDate")]
        [JsonProperty("createdDate")]
        public Nullable<DateTime> CreatedDate { get; set; }
        [JsonProperty("modifyDate")]
        [Column("ModifyDate")]
        public Nullable<DateTime> ModifyDate { get; set; }

        public virtual ICollection<PackagePracticeMaterials> packagePracticeMaterials { get; set; }


    }
}