using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.PurchageMgmt
{
    public class RMMaterialTotalCostDetail
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("RM_GRN_NO")]
        public string rmGrnNo { get; set; }
        [Column("Raw_Material_Group_Code")]
        public string rawMaterialGroupCode { get; set; }
        [Column("Raw_Material_Details_Code")]
        public string rawMaterialDetailsCode { get; set; }
        [Column("RM_GRN_Material_Wise_Total_Cost")]
        public decimal rmGrnMaterialWiseTotalCost { get; set; }
        [Column("RM_Batch_No")]
        public int rmBatchNo { get; set; }
        [Column("RM_GRN_Received_Qty")]
        public decimal rmGrnReceivedQty { get; set; }
        [Column("RM_Customs_Share_Amount")]
        public decimal? rmCustomsShareAmount { get; set; }
        [Column("RM_Packing_Share_Amount")]
        public decimal? rmPackingShareAmount { get; set; }
        [Column("RM_Freight_Share_Amount")]
        public decimal? rmFreightShareAmount { get; set; }
        [Column("RM_Insurance_Share_Amount")]
        public decimal? rmInsuranceShareAmount { get; set; }
        [Column("RM_GRN_Materialwise_Total_Cost")]
        public decimal rmGrnMaterialwiseTotalCost { get; set; }
        [Column("RM_GRN_Materialwise_Total_Rate")]
        public decimal rmGrnMaterialwiseTotalRate { get; set; }

    }
}