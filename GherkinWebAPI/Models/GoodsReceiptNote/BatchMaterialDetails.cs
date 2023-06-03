using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.GoodsReceiptNote
{
    public class BatchMaterialDetails
    {
        [Key]
        [Column("RM_GRN_Batch_No")]
        public int RM_GRN_Batch_No { get; set; }
        [Column("Batch_No_Lot_No")]
        public string Batch_No_Lot_No { get; set; }
        [Column("Mfg_Date")]
        public DateTime Mfg_Date { get; set; }
        [Column("Expiry_Date")]
        public DateTime Expiry_Date { get; set; }
        [Column("Bag_Pack_Size")]
        public decimal Bag_Pack_Size { get; set; }
        [Column("Bag_Pack_UOM")]
        public string Bag_Pack_UOM { get; set; }
        [Column("No_Bags_Packs")]
        public int No_Bags_Packs { get; set; }
        [Column("Total_Quantity")]
        public decimal Total_Quantity { get; set; }
        [Column("Total_Qty_UOM")]
        public string Total_Qty_UOM { get; set; }
        [Column("Bill_Rate")]
        public decimal Bill_Rate { get; set; }
        [Column("Total_Amount")]
        public decimal Total_Amount { get; set; }
        [Column("Free_Batch_No")]
        public string Free_Batch_No { get; set; }
        [Column("Free_Mfg_Date")]
        public DateTime Free_Mfg_Date { get; set; }
        [Column("Free_Expiry_Date")]
        public DateTime Free_Expiry_Date { get; set; }
        [Column("Free_Bag_Pack_Size")]
        public decimal Free_Bag_Pack_Size { get; set; }
        [Column("Free_Bag_Pack_UOM")]
        public string Free_Bag_Pack_UOM { get; set; }
        [Column("Free_No_Bags_Packs")]
        public int Free_No_Bags_Packs { get; set; }
        [Column("Free_Total_Quantity")]
        public decimal Free_Total_Quantity { get; set; }
        [Column("Gross_Total_Quantity")]
        public decimal Gross_Total_Quantity { get; set; }
        [Column("Gross_Total_Material_Amount")]
        public decimal Gross_Total_Material_Amount { get; set; }
        [Column("Discount_Amount")]
        public decimal Discount_Amount { get; set; }
        [Column("Net_Material_Amount")]
        public decimal Net_Material_Amount { get; set; }
        [Column("Received_Quantity")]
        public decimal Received_Quantity { get; set; }
        [Column("Net_Material_Rate")]
        public decimal Net_Material_Rate { get; set; }
    }
}