using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    [Table("Crop_Effective_Assocation_Rates")]
    public class CropAssociationRate
    {
        [Column("Id")]
        [Key]
        public int id { get; set; }
        //[Column("Farmers_Agreement_Code")]
        //public string FarmersAgreementCode { get; set; }
        [Column("Crop_Rate_No")]
        //[Key]
        public string Crop_Rate_No { get; set; }

        [Column("Crop_Scheme_Code")]
        public string CropSchemeCode { get; set; }

        [Column("Crop_Count_mm")]
        public Decimal CropCount { get; set; }

        [Column("Crop_Scheme_From")]
        public int CropSchemeFrom { get; set; }

        [Column("Crop_Scheme_Sign")]
        public string CropSchemeSign { get; set; }

        [Column("Crop_Rate_As_per_Association")]
        public decimal CropRateAsperAssociation { get; set; }        

        [Column("Crop_Rate_Per_UOM")]
        public string CropRatePerUOM { get; set; }

        public static implicit operator CropAssociationRate(List<CropAssociationRate> v)
        {
            throw new NotImplementedException();
        }
    }
}