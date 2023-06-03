using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.FarmersInputReturns
{
	public class FarmersInputsMaterialMaster
	{
        [Key]
        [Column("FIM_Return_No")]
        [JsonProperty("fIMReturnNo")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int FIMReturnNo { get; set; }

        [Column("FMIR_Date")]
        [JsonProperty("fMIRDate")]
        [Required]
        public DateTime FMIRDate { get; set; }

        [Column("Area_ID")]
        [JsonProperty("areaID")]
        [MaxLength(10)]
        [Required]
        public string AreaID { get; set; }

        [Column("Employee_ID")]
        [JsonProperty("employeeID")]
        [MaxLength(20)]
        [Required]
        public string EmployeeID { get; set; }

        [Column("Crop_Group_Code")]
        [JsonProperty("cropGroupCode")]
        [MaxLength(10)]
        [Required]
        public string CropGroupCode { get; set; }

        [Column("Crop_Name_Code")]
        [JsonProperty("cropNameCode")]
        [MaxLength(10)]
        [Required]
        public string CropNameCode { get; set; }

        [Column("PS_Number")]
        [JsonProperty("psNumber")]
        [MaxLength(10)]
        [Required]
        public string PSNumber { get; set; }

        [Column("Farmer_Code")]
        [JsonProperty("farmerCode")]
        [MaxLength(20)]
        [Required]
        public string FarmerCode { get; set; }

        [Column("FIM_Return_Voucher_No")]
        [JsonProperty("fIMReturnVoucherNo")]
        [MaxLength(35)]
        public string FIMReturnVoucherNo { get; set; }

        [Column("Stock_Return_Status")]
        [JsonProperty("stockReturnStatus")]
        [MaxLength(30)]
        public string StockReturnStatus { get; set; }

        [Column("Org_Office_No")]
        [JsonProperty("orgOfficeNo")]
        public int? OrgOfficeNo { get; set; }


        [NotMapped]
        [JsonProperty("fieldStaffAreaName")]
        public string FieldStaffAreaName { get; set; }

        [NotMapped]
        [JsonProperty("fieldStaffEmployeeName")]
        public string FieldStaffEmployeeName { get; set; }

        [NotMapped]
        [JsonProperty("cropGroupName")]
        public string CropGroupName { get; set; }

        [NotMapped]
        [JsonProperty("cropName")]
        public string CropName { get; set; }

        [NotMapped]
        [JsonProperty("seasonFromTo")]
        public string SeasonFromTo { get; set; }

        [NotMapped]
        [JsonProperty("villageCode")]
        public int VillageCode { get; set; }

        [NotMapped]
        [JsonProperty("villageName")]
        public string VillageName { get; set; }

        [NotMapped]
        [JsonProperty("farmerName")]
        public string FarmerName { get; set; }

        [NotMapped]
        [JsonProperty("farmerAltContactPerson")]
        public string FarmerAltContactPerson { get; set; }

        [NotMapped]
        [JsonProperty("orgOfficeName")]
        public string OrgOfficeName { get; set; }

    }
}