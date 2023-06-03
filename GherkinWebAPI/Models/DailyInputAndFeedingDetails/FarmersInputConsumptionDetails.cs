using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace GherkinWebAPI.Models.DailyInputAndFeedingDetails
{
    [Table("Farmers_Input_Consumption_Details")]
    public class FarmersInputConsumptionDetails
    {
        [JsonProperty("mifConsumptionNo")]
        [Column("MIF_Consumption_No")]
        [Key]
        public int MIFConsumptionNo { get; set; }

        [JsonProperty("mifDateofIssue")]
        [Column("MIF_Date_of_Issue")]
        public DateTime MIFDateofIssue { get; set; }


        [JsonProperty("areaID")]
        [Column("Area_ID")]
        public string AreaID { get; set; }

        [JsonProperty("mifEnteredEmpID")]
        [Column("MIF_Entered_Emp_ID")]
        public string MIFEnteredEmpID { get; set; }

        [JsonProperty("employeeID")]
        [Column("Employee_ID")]
        public string EmployeeID { get; set; }

        [JsonProperty("psNumber")]
        [Column("PS_Number")]
        public string PSNumber { get; set; }

        [JsonProperty("mifConsumptionVoucherNo")]
        [Column("MIF_Consumption_Voucher_No")]
        public string MIFConsumptionVoucherNo { get; set; }

        [JsonProperty("farmerCode")]
        [Column("Farmer_Code")]
        public string FarmerCode { get; set; }

        [JsonProperty("cropNameCode")]
        [Column("Crop_Name_Code")]
        public string CropNameCode { get; set; }

    }
}