using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.PackageOfPracticeModel
{
    [Table("HBOM_Package_Practice_Master")]
    public class PackagePracticeMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Required]       
        [Column("HS_Entry_Date")]
        [JsonProperty("entryDate")]
        public DateTime EntryDate { get; set; }
        [Required]
        [MaxLength(10)]
        [JsonProperty("employeeID")]
        [Column("HBOM_Entered_Employee_ID_By")]
        public string EmployeeID { get; set; }
        [Column("Crop_Group_Code")]
        [Required]
        [MaxLength(10)]
        [JsonProperty("cropgroupCode")]
        public string CropGroupCode { get; set; }
        [Column("Crop_Name_Code")]
        [Required]
        [MaxLength(10)]
        [JsonProperty("cropnameCode")]
        public string CropNameCode { get; set; }
        [Column("CreatedDate")]
        [JsonProperty("createdDate")]
        public Nullable<DateTime> CreatedDate { get; set; }
        [JsonProperty("modifyDate")]
        [Column("ModifyDate")]
        public Nullable<DateTime> ModifyDate { get; set; }


    }
}