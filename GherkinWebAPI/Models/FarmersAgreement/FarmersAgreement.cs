using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace GherkinWebAPI.Models
{
    public class FarmersAgreement
    {
        public FarmersAgreement()
        {
            FarmersAgreementSizes = new List<FarmersAgreementSize>();
        }
        public DateTime FarmersAgreementDate { get; set; }
        public string FarmersAgreementCode { get; set; }
        public string AreaID { get; set; }
        public int AreaCode { get; set; }
        public string EmployeeID { get; set; }
        public int VillageCode { get; set; }
        public string VillageName { get; set; }
        public string FarmerCode { get; set; }
        public string FarmerName { get; set; }
        public string Address { get; set; }
        public string DistrictName { get; set; }
        public string MandalName { get; set; }
        public int DistrictCode { get; set; }
        public int StateCode { get; set; }
        public string StateName { get; set; }
        public int CountryCode { get; set; }
        public string CountryName { get; set; }
        public string FarmersAccountNo { get; set; }
        public string CropGroupCode { get; set; }
        public string CropNameCode { get; set; }
        public string PSNumber { get; set; }
        public decimal FarmersNoOfAcersArea { get; set; }
        public string AgricultureDripNonDrip { get; set; }
        public string BoarderCrop { get; set; }
        public string PreviousCrop { get; set; }
        public string MulchingSheet { get; set; }
        public string FYM { get; set; }
        public List<FarmersAgreementSize> FarmersAgreementSizes { get; set; }
    }

    public class FarmersAgreementSize
    {
        public int ID { get; set; }
        public string CropSchemeCode { get; set; }
        public decimal CropCount { get; set; } 
        public string CropSchemeFromSign { get; set; }
        public decimal? CropRateAsPerAssociation { get; set; }
        public string CropRatePerUOM { get; set; }
        public decimal CropRateAsPerOurAgreement { get; set; }
        public string CropRatesRemarks { get; set; }
    }
}