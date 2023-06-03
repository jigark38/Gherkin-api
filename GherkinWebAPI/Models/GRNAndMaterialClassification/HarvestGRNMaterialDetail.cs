using System;
using System.Collections.Generic;

namespace GherkinWebAPI.Models.GRNAndMaterialClassification
{
    public class HarvestGRNMaterialDetail
    {
        public string AreaId { get; set; }
        public string EmployeeId { get; set; }
        public DateTime HarvestGRNDate { get; set; }
        public long HarvestGRNNo { get; set; }
        public string VechicalNo { get; set; }
        public string DriverName { get; set; }
        public long? DriverContactNo { get; set; }
        public int VechicalStartingReading { get; set; }
        public DateTime VehicalStartTime { get; set; }
        public int VehicalFreight { get; set; }
        public decimal HarvestGRNTotalQuantity { get; set; }
        public int HarvestGRNTotalDespCrates { get; set; }
        public int OrgOfficeNo { get; set; }
        public string HarvestGRNRemarks { get; set; }
        public int? GreensTransVehicleDespNo { get; set; }
        public List<HarvestGRNFarmersDetail> HarvestGRNFarmersDetails { get; set; }
        public List<HarvestGRNCratesDetail> HarvestGRNCratesDetails { get; set; }
    }

    public class HarvestGRNFarmersDetail
    {
        public int HarvestFarmerEntryNo { get; set; }
        public string CropSchemeCode { get; set; }
        public string CropNameCode { get; set; }
        public int NoOfCrates { get; set; }
        public decimal FarmerWiseTotalQuantity { get; set; }
    }

    public class HarvestGRNCratesDetail
    {
        public string CropSchemeCode { get; set; }
        public string CropNameCode { get; set; }
        public int HarvestGRNCratesDespatchNos { get; set; }
        public decimal HarvestGRNTotalApproxQty { get; set; }
    }
}