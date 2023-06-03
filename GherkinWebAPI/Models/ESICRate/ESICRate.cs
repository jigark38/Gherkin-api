using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class ESICRate
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("ESI_Passing_No")]
        [JsonProperty("esiPassingNo")]
        public Int64 ESI_Passing_No { get; set; }

        [Column("Entry_Date")]
        [JsonProperty("entryDate")]
        public DateTime Entry_Date { get; set; }

        [Column("Entered_Emp_ID")]
        [JsonProperty("enteredEmpId")]
        public string Entered_Emp_ID { get; set; }

        [Column("ESI_Effective_Date")]
        [JsonProperty("esiEffectiveDate")]
        public DateTime ESI_Effective_Date { get; set; }

        [Column("ESI_Effective_From_Date")]
        [JsonProperty("esiEffectiveFromDate")]
        public DateTime ESI_Effective_From_Date { get; set; }

        [Column("ESI_Effective_To_Date")]
        [JsonProperty("esiEffectiveToDate")]
        public DateTime ESI_Effective_To_Date { get; set; }

        [Column("ESI_Max_Limit")]
        [JsonProperty("esiMaxLimit")]
        public int ESI_Max_Limit { get; set; }

        [Column("ESI_Employee_Contr")]
        [JsonProperty("esiEmployeeCount")]
        public decimal ESI_Employee_Contr { get; set; }

        [Column("ESI_Employer_Contr")]
        [JsonProperty("esiEmployerCount")]
        public decimal ESI_Employer_Contr { get; set; }

        [Column("ESI_Total_Contr")]
        [JsonProperty("esiTotalCount")]
        public decimal ESI_Total_Contr { get; set; }

    }
}