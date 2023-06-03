using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.GRNAndMaterialClassification
{
    public class GreensReceivedDetail
    {
        public string AreaId { get; set; }
        public DateTime HarvestDate { get; set; }
        public long HarvestProcurementNo { get; set; }
        public string CropNameCode { get; set; }
        public string PSNumber { get; set; }
        public long HarvestFarmersEntryNo { get; set; }
        public string FarmerCode { get; set; }
        public string CropSchemeCode { get; set; }
        public int FarmerwiseTotalCrates { get; set; }
        public decimal FarmerwiseTotalQuantity { get; set; }
        public string FarmerName { get; set; }
        public int VillageCode { get; set; }
        public string FarmersAgreementCode { get; set; }
        public string FarmersAccountNo { get; set; }
        public string VillageName { get; set; }
        public int CropSchemeFrom { get; set; }
        public string CropSchemeSign { get; set; }
        public string CropGroupCode { get; set; }
        public string BuyerEmployeeID { get; set; }
        public string BuyerEmployeeName { get; set; }
        public string VehicleNo { get; set; }
    }
}