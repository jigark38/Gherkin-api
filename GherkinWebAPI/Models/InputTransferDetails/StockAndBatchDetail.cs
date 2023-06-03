using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.InputTransferDetails
{
    public class StockAndBatchDetail
    {
        public string flag { get; set; }      
        public List<firstFilled> firstFields { get; set; }
        public List<secondFilled> secondFields { get; set; }
    }

    public class firstFilled
    {
        public DateTime stockDate { get; set; }
        public string stockNo { get; set; }
        public string orgOfficeNo { get; set; }
        public string rawMaterialGroupCode { get; set; }
        public string rawMaterialDetailsCode { get; set; }
        public string rawMaterialUOM { get; set; }
        public string rmStockTotalDetailQty { get; set; }
        public decimal rawMaterialTotalQty { get; set; }
        public decimal rawMaterialTotalAmount { get; set; }
        public DateTime rmStockLOTGRNDate { get; set; }
        public string rmStockLOTGRNNo { get; set; }
        public decimal rmStockLotGrnQty { get; set; }
        public decimal rmStockLotGrnRate { get; set; }
        public decimal rmStockLotGrnAmount { get; set; }
        public string rmTransferNo { get; set; }
        public DateTime rmTransferDate { get; set; }
        public decimal rmMaterialTransferQty { get; set; }

        public decimal sumRMMaterialTransferQty { get; set; }

    }

    public class secondFilled
    {
        public int rmBatchNo { get; set; }
        public DateTime rmGrnDate { get; set; }
        public string rmGrnNo { get; set; }
        public string rawmaterialGroupCode { get; set; }
        public string rawMaterialDetailsCode { get; set; }
        public decimal rmGRNreceivedQty { get; set; }
        public decimal rmGRNMaterialWiseTotalCost { get; set; }
        public decimal rmGRNMaterialWiseTotalRate { get; set; }
        public string rmTransferNo { get; set; }
        public DateTime rmTransferDate { get; set; }
        public decimal rmMaterialTransferQty { get; set; }
        public decimal sumRMMaterialTransferQty { get; set; }

    }


    public class firstFilledResponse
    {
        public DateTime Stock_Date { get; set; }
        public string Stock_No { get; set; }
        public string Org_office_No { get; set; }
        public string Raw_Material_Group_Code { get; set; }
        public string Raw_Material_Details_Code { get; set; }
        public string Raw_Material_UOM { get; set; }
        public string RM_Stock_Total_Detailed_Qty { get; set; }
        public decimal Raw_Material_Total_QTY { get; set; }
        public decimal Raw_Material_Total_Amount { get; set; }
        public DateTime RM_Stock_LOT_GRN_Date { get; set; }
        public string RM_Stock_LOT_GRN_No { get; set; }
        public decimal RM_Stock_Lot_Grn_Qty { get; set; }
        public decimal RM_Stock_Lot_Grn_Rate { get; set; }
        public decimal RM_Stock_Lot_Grn_Amount { get; set; }
        public string RM_Transfer_No { get; set; }
        public DateTime RM_Transfer_Date { get; set; }
        public decimal RM_Material_Transfer_Qty { get; set; }

    }

    public class secondFilledResponse
    {
        public int RM_Batch_No { get; set; }
        public DateTime RM_GRN_Date { get; set; }
        public string RM_GRN_No { get; set; }
        public string Raw_Material_Group_Code { get; set; }
        public string Raw_Material_Details_Code { get; set; }
        public decimal RM_GRN_Received_Qty { get; set; }
        public decimal RM_GRN_Materialwise_Total_Cost { get; set; }
        public decimal RM_GRN_Materialwise_Total_Rate { get; set; }
        public string RM_Transfer_No { get; set; }
        public DateTime RM_Transfer_Date { get; set; }
        public decimal RM_Material_Transfer_Qty { get; set; }
    }

    public class CropDetailsByGroupCode
    {

        [JsonProperty("cropNameCode")]
        public string CropNameCode { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("cropGroupCode")]
        public string CropGroupCode { get; set; }

        [JsonProperty("cropGroupName")]
        public string CropGroupName { get; set; }

        [JsonProperty("cropSchemeCode")]
        public string CropSchemeCode { get; set; }

        [JsonProperty("cropSchemeFrom")]
        public int CropSchemeFrom { get; set; }

    }
}