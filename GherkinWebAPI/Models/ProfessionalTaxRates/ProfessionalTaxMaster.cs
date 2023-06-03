using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace GherkinWebAPI.Models.ProfessionalTaxRates
{
    public class ProfessionalTaxMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty("ptpassingNo")]
        [Column("PT_Passing_No")]
        public long PTPassingNo { get; set; }
        [JsonProperty("entryDate")]
        [Column("Entry_Date")]
        public DateTime EntryDate { get; set; }
        [JsonProperty("enteredEmpId")]
        [Column("Entered_Emp_ID")]
        public string EnteredEmpID { get; set; }
        [JsonProperty("pteffectiveDate")]
        [Column("PT_Effective_Date")]
        public DateTime PTEffectiveDate { get; set; }
        [JsonProperty("ptchallanPaymentDate")]
        [Column("PT_Challan_Payment_Date")]
        public DateTime PTChallanPaymentDate { get; set; }
        [JsonProperty("ptdirectorsPayable")]
        [Column("PT_Directors_Payable")]
        public int PTDirectorsPayable { get; set; }
        [JsonProperty("ptexemptedTillSalary")]
        [Column("PT_Exempted_Till_Salary")]
        public int PTExemptedTillSalary { get; set; }

        [JsonProperty("professionalTaxSlabsDetails")]
        public virtual ICollection<ProfessionalTaxSlabsDetail> ProfessionalTaxSlabsDetails { get; set; }
    }
}