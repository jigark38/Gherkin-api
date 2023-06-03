using GherkinWebAPI.Entities;
using GherkinWebAPI.Models.PackageOfPracticeModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.InputToFieldStaff
{
    public class InputToFieldStaffModel
    {
    }

    public class EmpInfoByHarvestArea
    {
        [JsonProperty("areaID")]
        public string Area_ID { get; set; }

        [JsonProperty("employeeID")]
        public string Employee_ID { get; set; }

        [JsonProperty("employeeStatus")]
        public string Employee_Status { get; set; } //Field Staff

        [JsonProperty("employeeName")]
        public string Employee_Name { get; set; }
    }

    public class CropGroupDetailsByAreaId
    {
        [JsonProperty("areaID")]
        public string Area_ID { get; set; }

        [JsonProperty("cropGroupCode")]
        public string Crop_Group_Code { get; set; }

        [JsonProperty("cropGroupName")]
        public string Crop_Group_Name { get; set; }

    }

    public class CropDetailsByGroupCode
    {
        [JsonProperty("cropGroupCode")]
        public string Crop_Group_Code { get; set; }

        [JsonProperty("cropGroupName")]
        public string Crop_Group_Name { get; set; }

        [JsonProperty("cropNameCode")]
        public string Crop_Name_Code { get; set; }

        [JsonProperty("cropName")]
        public string Crop_Name { get; set; }
    }

    public class PlantationSchDetailsByAreaID
    {
        [JsonProperty("areaID")]
        public string Area_ID { get; set; }

        [JsonProperty("fromDate")]
        public DateTime FromDate { get; set; }

        [JsonProperty("toDate")]
        public DateTime ToDate { get; set; }

        [JsonProperty("seasonFromToDate")]
        public string seasonFromToDate { get; set; }

        [JsonProperty("pSNumber")]
        public string PsNumber { get; set; }
    }

    public class HBOMMatDetailsByCropNameCode
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("cropNameCode")]
        public string Crop_Name_Code { get; set; }

        [JsonProperty("hbomDivisionFor")]
        public string HBOM_Division_For { get; set; }

        [JsonProperty("rawMaterialGroup")]
        public string Raw_Material_Group { get; set; }

        [JsonProperty("rawMaterialDetailsName")]
        public string Raw_Material_Details_Name { get; set; }

        [JsonProperty("hbomPracticePerAcreage")]
        public string HBOM_Practice_Per_Acreage { get; set; }

        [JsonProperty("hsTransactionCode")]
        public string HS_Transaction_Code { get; set; }

        [JsonProperty("hsCropPhaseCode")]
        public string HS_Crop_Phase_Code { get; set; }

        [JsonProperty("bomPracticeEffectiveDate")]
        public DateTime BOM_Practice_Effective_Date { get; set; }

        [JsonProperty("hbomPracticeNo")]
        public string HBOM_Practice_No { get; set; }

        [JsonProperty("psNumber")]
        public string PS_Number { get; set; }

        [JsonProperty("rawMaterialGroupCode")]
        public string Raw_Material_Group_Code { get; set; }

        [JsonProperty("rawMaterialDetailsCode")]
        public string Raw_Material_Details_Code { get; set; }

        [JsonProperty("hbomChemicalUOM")]
        public string HBOM_Chemical_UOM { get; set; }

        [JsonProperty("hbomChemicalVolume")]
        public decimal HBOM_Chemical_Volume { get; set; }

        [JsonProperty("materialPurchases")]
        public string Material_Purchases { get; set; }

        [JsonProperty("totalIssuedQty")]
        public string TotalIssuedQty { get; set; }

        [JsonProperty("totalAmountSum")]
        public decimal? TotalAmountSum { get; set; }

    }

    public class RM_Stock_MatDetails_A
    {
        [JsonProperty("rawMaterialGroupCode")] //from RM_Stock_Details where Stock_Date <= 'selected field 1 Transfer Date '. Binding with Stock_No get 
        public string Raw_Material_Group_Code { get; set; }

        [JsonProperty("rawMaterialDetailsCode")]
        public string Raw_Material_Details_Code { get; set; }
        [JsonProperty("stockDate")]
        public DateTime Stock_Date { get; set; }
        [JsonProperty("stockNo")]
        public string Stock_No { get; set; }
        [JsonProperty("orgofficeNo")]
        public string Org_office_No { get; set; }
        [JsonProperty("rawMaterialUOM")]
        public string Raw_Material_UOM { get; set; }
        [JsonProperty("rmStockTotalDetailedQty")]
        public string RM_Stock_Total_Detailed_Qty { get; set; }
        [JsonProperty("rawMaterialTotalQTY")]
        public decimal Raw_Material_Total_QTY { get; set; }
        [JsonProperty("rawMaterialTotalAmount")]
        public decimal Raw_Material_Total_Amount { get; set; }

        [JsonProperty("rmStockLOTGRNDate")]
        public DateTime RM_Stock_LOT_GRN_Date { get; set; } //from RM_Stock_Lot_Details. Binding with Stock_No and RM_Stock_LOT_GRN_No get 

        [JsonProperty("rmmStockLOTGRNNo")]
        public string RM_Stock_LOT_GRN_No { get; set; }
        [JsonProperty("rmStockLotGrnQty")]
        public decimal RM_Stock_Lot_Grn_Qty { get; set; }
        [JsonProperty("rmStockLotGrnRate")]
        public decimal RM_Stock_Lot_Grn_Rate { get; set; }
        [JsonProperty("rmStockLotGrnAmount")]
        public decimal RM_Stock_Lot_Grn_Amount { get; set; }

        [JsonProperty("rmTransferNo")]
        public string RM_Transfer_No { get; set; } //from RM_Input_Material_Transfer_Details where RM_Transfer_Date <= 'selected field 1 Transfer Date and 'RM_Stock_Lot_Grn_Qty > sum(RM_Material_Transfer_Qty)
        [JsonProperty("rmTransferDate")]
        public DateTime RM_Transfer_Date { get; set; }

        [JsonProperty("sumRmMaterialTransferQty")]
        public decimal sum_RM_Material_Transfer_Qty { get; set; }

        [JsonProperty("rmMaterialTransferQty")]
        public decimal RM_Material_Transfer_Qty { get; set; }

        [JsonProperty("rmGRNDate_B")]
        public DateTime RM_GRN_Date_B { get; set; }
        [JsonProperty("rmGRNNo_B")]
        public string RM_GRN_No_B { get; set; }

        //RM_Material_Total_Cost_Details
        [JsonProperty("rmBatchNo_B")]
        public int RM_Batch_No_B { get; set; }
        [JsonProperty("rmGRNReceivedQty_B")]
        public decimal RM_GRN_Received_Qty_B { get; set; }
        [JsonProperty("rmGRNMaterialwiseTotalCost_B")]
        public decimal RM_GRN_Materialwise_Total_Cost_B { get; set; }
        [JsonProperty("rmGRNMaterialwiseTotalRate_B")]
        public decimal RM_GRN_Materialwise_Total_Rate_B { get; set; }

        [JsonProperty("sumRmMaterialTransferQty_B")]
        public decimal sum_RM_Material_Transfer_Qty_B { get; set; }

        [JsonProperty("rmTransferNo_B")]
        public string RM_Transfer_No_B { get; set; }
        [JsonProperty("rmTransferDate_B")]
        public DateTime RM_Transfer_Date_B { get; set; }
        [JsonProperty("rmMaterialTransferQty_B")]
        public decimal RM_Material_Transfer_Qty_B { get; set; }

        [JsonProperty("enteredAmount")]
        public decimal EnteredAmount { get; set; }

        [JsonProperty("sumStockNoFSMatIssueQty")]
        public decimal sum_stockNo_FS_Material_Issued_Qty { get; set; }

        [JsonProperty("sumBatchNoFSMatIssueQty")]
        public decimal sum_BatchNo_FS_Material_Issued_Qty { get; set; }

        [JsonProperty("disabledGrid")]
        public bool? disabledGrid { get; set; }

        [JsonProperty("rmMatReturnedQty")]
        public decimal RM_Material_Return_Qty { get; set; }

        [JsonProperty("inputsIssuedFSDate")]
        public DateTime Inputs_Issued_FS_Date { get; set; }
    }


    public class Inputs_Issued_To_Fieldstaff_Materials
    {
        [JsonProperty("materialFSIssueID")]
        public int Material_FS_Issue_ID { get; set; }

        [JsonProperty("materialIssuedFSNo")]
        public string Material_Issued_FS_No { get; set; }

        [JsonProperty("inputsIssuedFSDate")]
        public DateTime Inputs_Issued_FS_Date { get; set; }

        [JsonProperty("ogpDate")]
        public DateTime OGP_Date { get; set; }

        [JsonProperty("hbomPracticeNo")]
        public string HBOM_Practice_No { get; set; }

        [JsonProperty("rawMaterialGroupCode")]
        public string Raw_Material_Group_Code { get; set; }

        [JsonProperty("rawMaterialDetailsCode")]
        public string Raw_Material_Details_Code { get; set; }

        [JsonProperty("stockNo")]
        public string Stock_No { get; set; }

        [JsonProperty("rmStockLOTGRNNo")]
        public string RM_Stock_LOT_GRN_No { get; set; }

        [JsonProperty("rmStockLotGrnRate")]
        public decimal? RM_Stock_Lot_Grn_Rate { get; set; }

        [JsonProperty("rmGRNNO")]
        public string RM_GRN_NO { get; set; }

        [JsonProperty("rmBatchNo")]
        public int? RM_Batch_No { get; set; }

        [JsonProperty("rmGRNMaterialwiseTotalRate")]
        public decimal? RM_GRN_Materialwise_Total_Rate { get; set; }

        [JsonProperty("FSMaterialIssuedQty")]
        public decimal FS_Material_Issued_Qty { get; set; }

        [JsonProperty("rmMaterialTransferAmount")]
        public decimal RM_Material_Transfer_Amount { get; set; }

        [JsonProperty("ogpNO")]
        public string OGP_NO { get; set; }

        [JsonProperty("orgofficeNo")]
        public int? Org_office_No { get; set; }

        [JsonProperty("areaID")]
        public string Area_ID { get; set; }

        [JsonProperty("employeeID")]
        public string Employee_ID { get; set; }

        [JsonProperty("cropGroupCode")]
        public string Crop_Group_Code { get; set; }

        [JsonProperty("cropNameCode")]
        public string Crop_Name_Code { get; set; }

        [JsonProperty("pSNumber")]
        public string PsNumber { get; set; }

        [JsonProperty("issuedByEmpId")]
        public string Issued_By_Employee_ID { get; set; }

    }

}