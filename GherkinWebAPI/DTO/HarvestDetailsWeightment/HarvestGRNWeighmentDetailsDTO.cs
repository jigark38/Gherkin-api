using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO
{
    public class HarvestGRNWeighmentDetailsDTO
    {
        [JsonProperty("unitHMInwardNo")]
        public long UnitHMInwardNo { get; set; }
        [JsonProperty("igpNo")]
        public string InwardGatePassNo { get; set; }
        [JsonProperty("orgID")]
        public int OfficeOrgID { get; set; }
        [JsonProperty("areaId")]
        public string AreaId { get; set; }
        [JsonProperty("harvestGRNNo")]
        public long HarvestGRNNo { get; set; }
        [JsonProperty("greenProcurementNo")]
        public int? Greens_Procurement_No { get; set; }
        [JsonProperty("cropGroupCode")]
        public string CropGroupCode { get; set; }
        [JsonProperty("cropNameCode")]
        public string CropNameCode { get; set; }
        [JsonProperty("supervisorId")]
        public string SupervisorId { get; set; }

        [JsonProperty("harvestGRNTotalDespCrates")]
        public int HarvestGRNTotalDespCrates { get; set; }
        [JsonProperty("harvestGRNTotalQuantity")]
        public decimal HarvestGRNTotalQuantity { get; set; }
        [JsonProperty("unitHarvestMaterialInwardDate")]
        public DateTime UnitHarvestMaterialInwardDate { get; set; }
        [JsonProperty("vehicleReachReading")]
        public int VehicleReachReading { get; set; }
        [JsonProperty("vehicleReachTime")]
        public TimeSpan VehicleReachTime { get; set; }
        [JsonProperty("vehicleTransitDuration")]
        public decimal VehicleTransitDuration { get; set; }
        [JsonProperty("vehicleTransitKms")]
        public int VehicleTransitKms { get; set; }
        [JsonProperty("totalReceivedCrates")]
        public int TotalReceivedCrates { get; set; }
        [JsonProperty("totalReceivedQantity")]
        public decimal TotalReceivedQty { get; set; }
        public IEnumerable<SummaryReceivingDetailsDTO> SummaryReceivingDetails { get; set; }
        public IEnumerable<SummaryWeighmentDetailsDTO> SummaryWeighmentDetails { get; set; }
    }
}