using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Entities.RMStock
{
    public class RMStockBranchQuantityDetails
    {
        [Key]
        public int ID { get; set; }

        public string Stock_No { get; set; }

        public string Raw_Material_Group_Code { get; set; }

        public string Raw_Material_Details_Code { get; set; }

        public string Raw_Material_UOM { get; set; }

        public decimal RM_Stock_Quantity { get; set; }
    }
}