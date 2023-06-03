using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models
{
    public class FarmersAgreementSizeDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Farmers_Agreement_Code { get; set; }
        public string Crop_Scheme_Code { get; set; }
        public decimal Crop_Count_mm { get; set; }
        public int Crop_Scheme_From { get; set; }
        public string Crop_Scheme_Sign { get; set; }
        public decimal? Crop_Rate_As_per_Association { get; set; }
        public string Crop_Rate_Per_UOM { get; set; }
        public decimal Crop_Rate_As_per_Our_Agreement { get; set; }
        public string Crop_Rates_Remarks { get; set; }

        public virtual FarmersAgreementDetail FarmersAgreementDetail { get; set; }
    }
}