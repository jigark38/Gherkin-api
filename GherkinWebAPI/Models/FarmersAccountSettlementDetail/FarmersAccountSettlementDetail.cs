using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.FarmerAccountDetailsFinalization
{
    public class FarmersAccountSettlementDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int FAS_No { get; set; }
        public string Farmer_Code { get; set; }
        public string Farmers_Account_No { get; set; }
        public string PS_Number { get; set; }
        public decimal Farmer_Balance_Amount { get; set; }
        public DateTime Farmers_AC_Settlement_Date { get; set; }
        public decimal Farmer_Incentive_Amount { get; set; }
        public decimal Farmer_Deduction_Amount { get; set; }
        public decimal Farmer_Final_Payable_Amount { get; set; }
        public string Farmer_Remarks_Details { get; set; }

    }
}