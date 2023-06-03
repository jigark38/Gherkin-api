using System;
using System.ComponentModel.DataAnnotations;

namespace GherkinWebAPI.Entities.RMStock
{
    public class RMStockLotDetails
    {
        [Key]
        public int RM_stock_Lot_Details_ID { get; set; }
        public string Stock_No { get; set; }

        public DateTime RM_Stock_LOT_GRN_Date { get; set; }

        public string RM_Stock_LOT_GRN_No { get; set; }

        public decimal RM_Stock_Lot_Grn_Qty { get; set; }

        public decimal RM_Stock_Lot_Grn_Rate { get; set; }

        public decimal RM_Stock_Lot_Grn_Amount { get; set; }
    }
}