using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class AdvanceCashIssuedToFarmersModel
    {
        [Column("AC_Entry_Date")]
        [JsonProperty("acEntryDate")]
        public DateTime ACEntryDate { get; set; }
        [Column("AC_Entered_Employee_ID")]
        [JsonProperty("acEnteredEmployeeID")]
        public string ACEnteredEmployeeID { get; set; }
        [Key]
        [Column("AC_Issued_No")]
        [JsonProperty("acIssuedNo")]
        public int ACIssuedNo { get; set; }
        [Column("AC_Issued_Date")]
        [JsonProperty("acIssuedDate")]
        public DateTime ACIssuedDate { get; set; }
        [Column("Area_ID")]
        [JsonProperty("areaID")]
        public string AreaID { get; set; }
        [Column("Field_Supervisor_Employee_ID")]
        [JsonProperty("fieldSupervisorEmployeeID")]
        public string FieldSupervisorEmployeeID { get; set; }
        [Column("Field_Staff_Employee_ID")]
        [JsonProperty("fieldStaffEmployeeID")]
        public string FieldStaffEmployeeID { get; set; }
        [Column("Farmers_Account_No")]
        [JsonProperty("farmersAccountNo")]
        public string FarmersAccountNo { get; set; }
        [Column("Farmer_Code")]
        [JsonProperty("farmerCode")]
        public string FarmerCode { get; set; }
        [Column("Advance_Amount")]
        [JsonProperty("advanceAmount")]
        public int AdvanceAmount { get; set; }

    }
}