using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GherkinWebAPI.Entities.RMStock
{
    public class RMStockDetails
    {
        public RMStockDetails()
        {
            RMStockLotDetailsList = new List<RMStockLotDetails>();
        }
        public int ID { get; set; }

        public DateTime Stock_Date { get; set; }

        [Key]
        public string Stock_No { get; set; }

        public string Org_office_No { get; set; }

        public string Nature_office_Details { get; set; }

        public string Raw_Material_Group_Code { get; set; }

        public string Raw_Material_Details_Code { get; set; }

        public string Raw_Material_UOM { get; set; }

        public string RM_Stock_Total_Detailed_Qty { get; set; }

        public decimal Raw_Material_Total_QTY { get; set; }

        public decimal Raw_Material_Total_Amount { get; set; }

        public List<RMStockLotDetails> RMStockLotDetailsList { get; set; }
    }
}