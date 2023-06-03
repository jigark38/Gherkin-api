using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace GherkinWebAPI.Models
{
    public class GreensGradingQuantityDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [JsonProperty("greensGradingQtyNo")]
        public int Greens_Grading_Qty_No { get; set; }

        [JsonProperty("greensGradeNo")]
        public int Greens_Grade_No { get; set; }

        [JsonProperty("cropNameCode")]
        public string Crop_Name_Code { get; set; }

        [JsonProperty("cropSchemeCode")]
        public string Crop_Scheme_Code { get; set; }

        [JsonProperty("gradingNoofCrates")]
        public int Grading_No_of_Crates { get; set; }

        [JsonProperty("quantityAfterGradingTotal")]
        public decimal Quantity_After_Grading_Total { get; set; }
        //[JsonProperty("GreensProcurementNo")]
        //public int? Greens_Procurement_No { get; set; }
    }

    public class GreensGradingWeighmentDetails
    {        
        [Key]
        [JsonProperty("gmWeightNo")]
        public long GM_Weight_No { get; set; }

        //[JsonProperty("HarvestGRNNo")]
        //public long Harvest_GRN_No { get; set; }

        [JsonProperty("greensGradeNo")]
        public int Greens_Grade_No { get; set; }

        [JsonProperty("cropNameCode")]
        public string Crop_Name_Code { get; set; }

        [JsonProperty("cropSchemeCode")]
        public string Crop_Scheme_Code { get; set; }

        [JsonProperty("gMWeightNoofCrates")]
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

    public class GreensGradingInwardDetails
    {
        public GreensGradingInwardDetails()
        {
            GreensGradedHarvestGRNDetailsList = new List<GreensGradedHarvestGRNDetails>();
            GreensGradingQuantityDetailsList = new List<GreensGradingQuantityDetails>();
            GreensGradingWeighmentDetailsList = new List<GreensGradingWeighmentDetails>();
        }
        [Key]
        [JsonProperty("greensGradeNo")]
        public int Greens_Grade_No { get; set; }

        [JsonProperty("orgofficeNo")]
        public int Org_office_No { get; set; }

        [JsonProperty("gradedTotalQuantity")]
        public decimal Graded_Total_Quantity { get; set; }

        [JsonProperty("weighmentMode")]
        public string Weighment_Mode { get; set; }

        [JsonProperty("cropNameCode")]
        public string Crop_Name_Code { get; set; }

        [JsonProperty("cropGroupCode")]
        public string Crop_Group_Code { get; set; }

        [JsonProperty("gradedTotalCrates")]
        public int Graded_Total_Crates { get; set; }
        [JsonProperty("greensGradedHarvestGRNDetailsList")]
        public List<GreensGradedHarvestGRNDetails> GreensGradedHarvestGRNDetailsList { get; set; }
        [JsonProperty("greensGradingQuantityDetailsList")]
        public List<GreensGradingQuantityDetails> GreensGradingQuantityDetailsList { get; set; }

        [JsonProperty("greensGradingWeighmentDetailsList")]
        public List<GreensGradingWeighmentDetails> GreensGradingWeighmentDetailsList { get; set; }
    }



    public class GreensGradedHarvestGRNDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [JsonProperty("gradedHarvestGRNNo")]
        public int Graded_Harvest_GRN_Nos { get; set; }

        [JsonProperty("greensGradeNo")]
        public int Greens_Grade_No { get; set; }

        [JsonProperty("harvestGRNNo")]
        public long Harvest_GRN_No { get; set; }
        [JsonProperty("greensProcurementNo")]
        public int Greens_Procurement_No { get; set; }

        [JsonProperty("areaId")]
        public string Area_ID { get; set; }

        [JsonProperty("status")]
        public bool? STATUS { get; set; }
    }

    public class GridOneResponse
    {
        [JsonProperty("harvestGRNNo")]
        public long? HarvestGRNNo { get; set; }
        [JsonProperty("greensProcurementNo")]
        public int? GreensProcurementNo { get; set; }
        [JsonProperty("areaID")]
        public string AreaID { get; set; }
        [JsonProperty("areaName")]
        public string AreaName { get; set; }
        [JsonProperty("totalReceivedCrates")]
        public int TotalReceivedCrates { get; set; }
        [JsonProperty("totalReceivedQty")]
        public decimal TotalReceivedQty { get; set; }
        [JsonProperty("harvestGRNDate")]
        public DateTime HarvestGRNDate { get; set; }
        [JsonProperty("cropNameCode")]
        public string CropNameCode { get; set; }
        [JsonProperty("cropSchemeCode")]
        public string CropSchemeCode { get; set; }
        [JsonProperty("cropName")]
        public string CropName { get; set; }
        [JsonProperty("unitHMInwardNo")]
        public long UnitHMInwardNo { get; set; }
        [JsonProperty("isOnGoing")]
        public bool? IsOnGoing { get; set; }
        [JsonProperty("greensGradeNo")]
        public int? GreensGradeNo { get; set; }
    }

    public class GridTwoResponse
    {
        public int? NoofCrates { get; set; }
        public decimal? GradewiseTotalQuantity { get; set; }
        public string Gardes { get; set; }
        public bool selectedRow { get; set; }
        public string CropSchemeCode { get; set; }
        public int CropSchemeFrom { get; set; }
        public string CropSchemeSign { get; set; }
        public long? HarvestGRNNo { get; set; }
        public int? GreensProcurementNo { get; set; }
    }

    public class GreensGradingInwardPostDetails
    {
        public int OrgOfficeNo { get; set; }
        public string AreaID { get; set; }
        public long HarvestGRNNo { get; set; }
        public decimal GradedTotalQuantity { get; set; }
        public string WeighmentMode { get; set; }

        public string CropNameCode { get; set; }
        public string CropSchemeCode { get; set; }

        public List<GreensGradingQuantityPostDetails> GreensGradingQuantityDetails { get; set; }
        public List<GreensGradingWeighmentPostDetails> GreensGradingWeighmentDetails { get; set; }
        public int? GreensProcurementNo { get; set; }
    }

    public class GreensGradingQuantityPostDetails
    {
        public int NoofCrates { get; set; }
        public decimal GradewiseTotalQuantity { get; set; }
        public decimal ActualQuantity { get; set; }
        public string CropSchemeCode { get; set; }
    }

    public class GreensGradingWeighmentPostDetails
    {
        public int NoOfCrates { get; set; }

        public decimal GrossWt { get; set; }

        public decimal TareWt { get; set; }

        public decimal NetWt { get; set; }
        public string CropSchemeCode { get; set; }
    }
}