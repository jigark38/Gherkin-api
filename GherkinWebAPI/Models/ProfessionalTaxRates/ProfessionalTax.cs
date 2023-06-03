using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace GherkinWebAPI.Models.ProfessionalTaxRates
{
    public class ProfessionalTax
    {
        [JsonProperty("pTPasssingNo")]
        public long PTPasssingNo { get; set; }
        [JsonProperty("dateofEntry")]
        public DateTime DateofEntry { get; set; }
        [JsonProperty("loginUserName")]
        public string LoginUserName { get; set; }
        [JsonProperty("effectiveDate")]
        public DateTime EffectiveDate { get; set; }
        [JsonProperty("monthlyChallanDate")]
        public DateTime MonthlyChallanDate { get; set; }
        [JsonProperty("directorsPayable")]
        public int DirectorsPayable { get; set; }
        [JsonProperty("taxAmountExemptedSalary")]
        public int TaxAmountExemptedSalary { get; set; }
        [JsonProperty("professionalTaxSlabs")]
        public List<ProfessionalTaxSlab> ProfessionalTaxSlabs { get; set; }
    }

    public class ProfessionalTaxSlab
    {
        [JsonProperty("pTPassingSlabID")]
        public long PTPassingSlabID { get; set; }
        [JsonProperty("salaryFrom")]
        public long SalaryFrom  { get; set; }
        [JsonProperty("salaryTo")]
        public int SalaryTo { get; set; }
        [JsonProperty("professionalTaxAmount")]
        public int ProfessionalTaxAmount { get; set; }
    }
}