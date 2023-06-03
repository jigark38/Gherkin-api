using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class HarvestGRNIMWeightDetails
    {
        [Key]
        public long HM_Weight_Inward_No { get; set; }
        public long Unit_HM_Inward_No { get; set; }
        public string Crop_Name_Code { get; set; }
        public string Crop_Scheme_Code { get; set; }
        public string Crop_Group_Code { get; set; }
        public int HM_Weight_No_of_Crates { get; set; }
        public decimal HM_Weight_Gross_Weight { get; set; }
        public decimal HM_Weight_Tare_Weight { get; set; }
        public decimal HM_Weight_Net_Weight { get; set; }
        public decimal HM_Weight_Crates_Tare_Weight { get; set; }
    }
}