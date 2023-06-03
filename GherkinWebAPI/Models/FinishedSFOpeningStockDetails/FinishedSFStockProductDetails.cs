using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class FinishedSFStockProductDetails
    {
        public FinishedSFStockProductDetails()
        {
            FinishedSFStockQuantityDetailsList = new List<FinishedSFStockQuantityDetails>();
        }
        [Column("FSF_OS_Stock_Entry_Date")]
        [JsonProperty("fSFOSStockEntryDate")]
        public DateTime FSFOSStockEntryDate { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("FSF_OS_Stock_No")]
        [JsonProperty("fSFOSStockNo")]
        public int FSFOSStockNo { get; set; }
        [Column("Employee_ID")]
        [JsonProperty("employeeID")]
        public string EmployeeID { get; set; }
        [Column("Org_office_No")]
        [JsonProperty("orgOfficeNo")]
        public int OrgOfficeNo { get; set; }
        [Column("Area_ID")]
        [JsonProperty("areaID")]
        public string AreaID { get; set; }
        [Column("FSF_Stock_Type")]
        [JsonProperty("fSFStockType")]
        public string FSFStockType { get; set; }
        [Column("FSF_Packing_Mode")]
        [JsonProperty("fSFPackingMode")]
        public string FSFPackingMode { get; set; }
        [Column("C_B_Code")]
        [JsonProperty("cBCode")]
        public string CBCode { get; set; }
        [Column("Prof_Inv_No")]
        [JsonProperty("profInvNo")]
        public string ProfInvNo { get; set; }
        [Column("FP_Group_Code")]
        [JsonProperty("fPGroupCode")]
        public string FPGroupCode { get; set; }
        [Column("FP_Variety_Code")]
        [JsonProperty("fPVarietyCode")]
        public string FPVarietyCode { get; set; }
        [Column("Production_Process_Code")]
        [JsonProperty("productionProcessCode")]
        public string ProductionProcessCode { get; set; }
        [Column("Media_Process_Code")]
        [JsonProperty("mediaProcessCode")]
        public string MediaProcessCode { get; set; }
        [Column("FP_Grade_Code")]
        [JsonProperty("fPGradeCode")]
        public string FPGradeCode { get; set; }
        [JsonProperty("finishedSFStockProductDetailsList")]
        public List<FinishedSFStockQuantityDetails> FinishedSFStockQuantityDetailsList { get; set; }
    }
}