using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class HarvestGRNInwardMaterialDetails
    {
        [Key]
        public long Harvest_GRN_Inward_Material_No { get; set; }
        public long Unit_HM_Inward_No { get; set; }
        public long Harvest_GRN_No { get; set; }
        public string Crop_Name_Code { get; set; }
        public string Crop_Scheme_Code { get; set; }
        public string Crop_Group_Code { get; set; }
        public int No_of_Crates { get; set; }
        public decimal Grade_wise_Total_Quantity { get; set; }
        public int? Greens_Procurement_No { get; set; }
    }
}