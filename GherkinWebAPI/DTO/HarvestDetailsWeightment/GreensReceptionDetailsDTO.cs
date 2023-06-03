using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO
{
    public class GreensReceptionDetailsDTO
    {
        [JsonProperty("orgID")]
        public int? OrgID { get; set; }
        [JsonProperty("harvestGRNDate")]
        public DateTime HarvestGRNDate { get; set; }

        [JsonProperty("harvestGRNNo")]
        public Int64? HarvestGRNNo { get; set; }
        [JsonProperty("areaId")]
        public string AreaId { get; set; }
        [JsonProperty("areaName")]
        public string Area { get; set; }

        [JsonProperty("veichleNo")]
        public string VehicleNo { get; set; }
        [JsonProperty("cropGroupCode")]
        public string CropGroupCode { get; set; }
        [JsonProperty("cropCode")]
        public string CropCode { get; set; }

        [JsonProperty("cropName")]
        public string CropName { get; set; }
        [JsonProperty("cropSchemeCode")]
        public string CropSchemeCode { get; set; }
        [JsonProperty("cropSchemeFrom")]
        public int CropSchemeFrom { get; set; }
        [JsonProperty("cropSchemeSign")]
        public string CropSchemeSign { get; set; }
        [JsonProperty("cropCountmm")]
        public decimal CropCountmm { get; set; }

        [JsonProperty("noOfCrates")]
        public int? NoofCrates { get; set; }
        [JsonProperty("farmerWiseTotalQuantity")]
        public decimal FarmerWiseTotalQuantity { get; set; }
        [JsonProperty("harvestGRNTotalQuantity")]
        public decimal? HarvestGRNTotalQuantity { get; set; }
        [JsonProperty("startingKMs")]
        public string VehicleStartingReading { get; set; }
        [JsonProperty("startiTime")]
        public DateTime VeichleStartTime { get; set; }
        [JsonProperty("despNoOfCrates")]
        public int? HarvestGRNTotalDespCrates { get; set; }
        [JsonProperty("grades")]
        public List<GradeDTO> Grades { get; set; }
        [JsonProperty("greenProcurementNo")]
        public int? GreenProcurementNo { get; set; }
        [JsonProperty("startigreenProcurementTime")]
        public TimeSpan VeichleGreenProcurementStartTime { get; set; }

        public string BuyerEmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public DateTime InwardDateTime { get; set; }

        public decimal Grade_wise_Total_Quantity { get; set; }

        public int? HarvestProcurementNumber { get; set; }


        public string AgentOrganisationName { get; set; }

        public string PlaceName { get; set; }

        public decimal Count_Total_Weight { get; set; }
    }
}