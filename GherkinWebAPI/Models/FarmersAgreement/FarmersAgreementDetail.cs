using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models
{
    public class FarmersAgreementDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime Farmers_Agreement_Date { get; set; }
        [Key]
        public string Farmers_Agreement_Code { get; set; }
        public string Area_ID { get; set; }
        public int Area_Code { get; set; }
        public string Employee_ID { get; set; }
        public int Village_Code { get; set; }
        public string Farmer_Code { get; set; }
        public string Farmers_Account_No { get; set; }
        public string Crop_Group_Code { get; set; }
        public string Crop_Name_Code { get; set; }
        public string PS_Number { get; set; }
        public decimal Farmers_No_of_Acres_Area { get; set; }
        public string Agriculture_DRIP_NONDRIP { get; set; }
        public string Boarder_Crop { get; set; }
        public string Previous_Crop { get; set; }
        public string Mulching_Sheet { get; set; }
        public string FYM { get; set; }

        public virtual ICollection<FarmersAgreementSizeDetail> FarmersAgreementSizeDetails { get; set; }
    }
}