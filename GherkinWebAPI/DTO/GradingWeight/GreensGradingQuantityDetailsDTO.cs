using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace GherkinWebAPI.DTO
{
    public class GreensGradingQuantityDetailsDTO
    {
        [JsonProperty("greensGradingQtyNo")]
        public int Greens_Grading_Qty_No { get; set; }

        [JsonProperty("greensGradeNo")]
        public int Greens_Grade_No { get; set; }

        [JsonProperty("cropNameCode")]
        public string Crop_Name_Code { get; set; }
        [JsonProperty("cropName")]
        public string Crop_Name { get; set; }

        [JsonProperty("cropSchemeCode")]
        public string Crop_Scheme_Code { get; set; }
        [JsonProperty("from")]
        public int From { get; set; }
        [JsonProperty("sign")]
        public string Sign { get; set; }
        [JsonProperty("count")]
        public decimal Count { get; set; }

        [JsonProperty("gradingNoofCrates")]
        public int Grading_No_of_Crates { get; set; }

        [JsonProperty("quantityAfterGradingTotal")]
        public decimal Quantity_After_Grading_Total { get; set; }
        //[JsonProperty("GreensProcurementNo")]
        //public int? Greens_Procurement_No { get; set; }
    }

    public class GreensGradingWeighmentDetailsDTO
    {
        [JsonProperty("gmWeightNo")]
        public long GM_Weight_No { get; set; }

        //[JsonProperty("HarvestGRNNo")]
        //public long Harvest_GRN_No { get; set; }

        [JsonProperty("greensGradeNo")]
        public int Greens_Grade_No { get; set; }

        [JsonProperty("cropNameCode")]
        public string Crop_Name_Code { get; set; }
        [JsonProperty("cropName")]
        public string Crop_Name { get; set; }
        [JsonProperty("cropSchemeCode")]
        public string Crop_Scheme_Code { get; set; }
        [JsonProperty("from")]
        public int From { get; set; }
        [JsonProperty("sign")]
        public string Sign { get; set; }
        [JsonProperty("count")]
        public decimal Count { get; set; }

        [JsonProperty("gmWeightNoofCrates")]
        public int GM_Weight_No_of_Crates { get; set; }

        [JsonProperty("gmWeightGrossWeight")]
        public decimal GM_Weight_Gross_Weight { get; set; }

        [JsonProperty("gmWeightTareWeight")]
        public decimal GM_Weight_Tare_Weight { get; set; }

        [JsonProperty("hmWeightNetWeight")]
        public decimal HM_Weight_Net_Weight { get; set; }

        [JsonProperty("gmCratesTareWeight")]
        public decimal GM_Crates_Tare_Weight { get; set; }
        //[JsonProperty("GreensProcurementNo")]
        //public int? Greens_Procurement_No { get; set; }
    }

    public class GreensGradingInwardDetailsDTO
    {
        public GreensGradingInwardDetailsDTO() 
        {
            GreensGradedHarvestGRNDetailsList = new List<GreensGradedHarvestGRNDetailsDTO>();
            GreensGradingQuantityDetailsList = new List<GreensGradingQuantityDetailsDTO>();
            GreensGradingWeighmentDetailsList = new List<GreensGradingWeighmentDetailsDTO>();
        }
        [JsonProperty("greensGradeNo")]
        public int Greens_Grade_No { get; set; }

        [JsonProperty("orgofficeNo")]
        public int Org_office_No { get; set; }
        [JsonProperty("orgOfficeName")]
        public string Org_office_Name { get; set; }

        [JsonProperty("areaID")]
        public string Area_ID { get; set; }
        [JsonProperty("areaName")]
        public string Area_Name { get; set; }

        [JsonProperty("harvestGRNNo")]
        public long Harvest_GRN_No { get; set; }

        [JsonProperty("gradedTotalQuantity")]
        public decimal Graded_Total_Quantity { get; set; }

        [JsonProperty("weighmentMode")]
        public string Weighment_Mode { get; set; }
        [JsonProperty("greensProcurementNo")]
        public int? Greens_Procurement_No { get; set; }

        [JsonProperty("cropNameCode")]
        public string Crop_Name_Code { get; set; }
        [JsonProperty("cropName")]
        public string Crop_Name { get; set; }

        [JsonProperty("cropGroupCode")]
        public string Crop_Group_Code { get; set; }

        [JsonProperty("cropGroupName")]
        public string Crop_Group_Name { get; set; }

        [JsonProperty("gradedTotalCrates")]
        public int Graded_Total_Crates { get; set; }

        [JsonProperty("greensGradedHarvestGRNDetailsList")]
        public List<GreensGradedHarvestGRNDetailsDTO> GreensGradedHarvestGRNDetailsList { get; set; }
        [JsonProperty("greensGradingQuantityDetailsList")]
        public List<GreensGradingQuantityDetailsDTO> GreensGradingQuantityDetailsList { get; set; }

        [JsonProperty("greensGradingWeighmentDetailsList")]
        public List<GreensGradingWeighmentDetailsDTO> GreensGradingWeighmentDetailsList { get; set; }
    }

    public class GreensGradedHarvestGRNDetailsDTO
    {
        [JsonProperty("gradedHarvestGRNNo")]
        public int Graded_Harvest_GRN_Nos { get; set; }

        [JsonProperty("greensGradeNo")]
        public int Greens_Grade_No { get; set; }

        [JsonProperty("greensProcurementNo")]
        public int Greens_Procurement_No { get; set; }

        [JsonProperty("harvestGRNNo")]
        public long Harvest_GRN_No { get; set; }
        [JsonProperty("cropName")]
        public string CropName { get; set; }

        [JsonProperty("areaId")]
        public string Area_ID { get; set; }
        [JsonProperty("areaName")]
        public string Area_Name { get; set; }

        [JsonProperty("status")]
        public bool? STATUS { get; set; }
    }
    
}