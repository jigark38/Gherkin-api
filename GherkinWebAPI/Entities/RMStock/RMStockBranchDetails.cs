using System;
using System.ComponentModel.DataAnnotations;

namespace GherkinWebAPI.Entities.RMStock
{
    public class RMStockBranchDetails
    {
        public int ID { get; set; }

        [Key]
        public string Stock_No { get; set; }

        public DateTime Stock_Date { get; set; }

        public string Area_ID { get; set; }

        public int Area_Code { get; set; }
    }
}