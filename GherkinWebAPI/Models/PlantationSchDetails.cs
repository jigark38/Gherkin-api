using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    [Table("Crop_Name_Details")]
    public class PlantationSchDetails
    {
        [Key]
        [Column("PS_Number")]
        public string PS_Number { get; set; }
        [Column("PS_Date")]
        public DateTime PS_Date { get; set; }
        [Column("Crop_Group_Code")]
        public string CropGroupCode { get; set; }
        [Column("Crop_Name_Code")]
        public string CropNameCode { get; set; }
        [Column("Season_From_Date")]
        public DateTime SeasonFromDate { get; set; }
        [Column("Season_To_Date")]
        public DateTime SeasonToDate { get; set; }
        [Column("PS_Prepared_By_Employee_ID")]
        public string PSPreparedByEmpId { get; set; }
        [Column("PS_Approved_By_Employee_ID")]
        public string PSApprovedByEmpId { get; set; }
    }
}