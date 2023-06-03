using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.BatchProduction
{
    public class BatchGreenDetailsDto
    {
        [JsonProperty("harvestGRNNo")]
        public long Harvest_GRN_No { get; set; }
        [JsonProperty("harvestGRNDate")]
        public DateTime Harvest_GRN_Date { get; set; }
        [JsonProperty("orgOfficeNo")]
        public int OrgOfficeNo { get; set; }

        [JsonProperty("GreensGradeNo")]
        public int? Greens_Grade_No { get; set; }
        [JsonProperty("GreensGradingQtyNo")]
        public int? Greens_Grading_Qty_No { get; set; }

        [JsonProperty("grades")]
        public string Grades { get; set; }

        [JsonProperty("cropName")]
        public string Crop_Name { get; set; }
        [JsonProperty("cropCode")]
        public string Crop_Name_Code { get; set; }

        [JsonProperty("areaName")]
        public string Area_Name { get; set; }

        [JsonProperty("CropSchemeCode")]
        public string Crop_Scheme_Code { get; set; }

        [JsonProperty("GradingNoofCrates")]
        public int? Grading_No_of_Crates { get; set; }

        [JsonProperty("QuantityAfterGradingTotal")]
        public decimal? Quantity_After_Grading_Total { get; set; }

        [JsonProperty("mediaProcessCode")]
        public string MediaProcessCode { get; set; }

        [JsonProperty("mediaProcessName")]
        public string MediaProcessName { get; set; }
    }
}