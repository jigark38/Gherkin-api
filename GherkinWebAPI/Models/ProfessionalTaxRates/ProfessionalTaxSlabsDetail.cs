using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace GherkinWebAPI.Models.ProfessionalTaxRates
{
    public class ProfessionalTaxSlabsDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty("ptsalarySlabId")]
        [Column("PT_Salary_Slab_ID")]
        public long PTSalarySlabID { get; set; }
        [JsonProperty("ptpassingNo")]
        [Column("PT_Passing_No")]
        public long PTPassingNo { get; set; }
        [JsonProperty("ptsalaryFrom")]
        [Column("PT_Salary_From")]
        public long PTSalaryFrom { get; set; }
        [JsonProperty("ptsalaryTo")]
        [Column("PT_Salary_To")]
        public int PTSalaryTo { get; set; }
        [JsonProperty("ptemployeesAmountPayable")]
        [Column("PT_Employees_Amount_Payable")]
        public int PTEmployeesAmountPayable { get; set; }

        public virtual ProfessionalTaxMaster ProfessionalTaxMaster { get; set; }

    }
}