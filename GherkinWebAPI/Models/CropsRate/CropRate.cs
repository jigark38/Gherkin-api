using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    [Table("Crop_Rate_Details")]
    public class CropRate
    {
        [Column("Crop_Rate_Entry_Date")]
        public DateTime CropRateEntryDate { get; set; }
        
        [Column("Crop_Rate_Entered_By_Emp_Id")]
        public string CropRateEnteredByEmpId { get; set; }
        [Column("Crop_Rate_No")]
        [Key]
        public string Crop_Rate_No { get; set; }
        [Column("Crop_Group_Code")]
        public string CropGroupCode { get; set; }

        [Column("Crop_Name_Code")]
        public string CropGroupName { get; set; }
        [Column("All_Areas")]
        public string AllAreas { get; set; }
        [Column("Area_ID")]
        public string AreaId { get; set; }

        [Column("All_Villages")]
        public string AllVillages { get; set; }

        [Column("Village_Code")]
        public string VillageCode { get; set; }

        [Column("Crop_Scheme_Code")]
        public string CropSchemeCode { get; set; }

        [Column("Crop_Rate_Effective_Date")]
        public DateTime CropRateEffectiveDate { get; set; }

        [Column("PS_Number")]
        public string PSNumber { get; set; }
    }
}