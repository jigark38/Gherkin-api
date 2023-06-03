using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.GRPAndroid.ProdProcessBOM
{
    [Table("Production_Process_Details")]
    public class ProductionProcessDetails
    {
        [Key]
        [JsonProperty("productionProcessCode")]
        [Column("Production_Process_Code")]
        public string ProductionProcessCode { get; set; }

        [JsonProperty("dateofCreation")]
        [Column("Date_of_Creation")]
        public DateTime DateofCreation { get; set; }

        [JsonProperty("employeeID")]
        [Column("Employee_ID")]
        public string EmployeeID { get; set; }

        [JsonProperty("fpGroupCode")]
        [Column("FP_Group_Code")]
        public string FPGroupCode { get; set; }

        [JsonProperty("fpVarietyCode")]
        [Column("FP_Variety_Code")]
        public string FPVarietyCode { get; set; }

        [JsonProperty("productionProcessName")]
        [Column("Production_Process_Name")]
        public string ProductionProcessName { get; set; }

        [JsonProperty("productionProcessDescription")]
        [Column("Production_Process_Description")]
        public string ProductionProcessDescription { get; set; }

    }
}