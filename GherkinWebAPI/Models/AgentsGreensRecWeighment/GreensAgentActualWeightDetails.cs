using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.AgentsGreensRecWeighment
{
    public class GreensAgentActualWeightDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Actual_Count_Weight_No")]
        [JsonProperty("actualCountWeightNo")]
        [Key]
        public int ActualCountWeightNo { get; set; }
        [Column("Greens_Agent_GRN_No")]
        [JsonProperty("greensAgentGRNNo")]
        public int GreensAgentGRNNo { get; set; }

        [Column("Crop_Name_Code")]
        [JsonProperty("cropNameCode")]
        public string CropNameCode { get; set; }

        [Column("Crop_Scheme_Code")]
        [JsonProperty("cropSchemeCode")]
        public string CropSchemeCode { get; set; }

        [Column("Actual_Weight_No_of_Crates")]
        [JsonProperty("actualWeightNoofCrates")]
        public int ActualWeightNoofCrates { get; set; }

        [Column("Actual_Crates_Tare_Weight")]
        [JsonProperty("actualCratesTareWeight")]
        public decimal ActualCratesTareWeight { get; set; }

        [Column("Sl_No_From")]
        [JsonProperty("slNoFrom")]
        public string SlNoFrom { get; set; }

        [Column("Sl_No_To")]
        [JsonProperty("slNoTo")]
        public string SlNoTo { get; set; }

        [Column("Actual_Gross_Weight")]
        [JsonProperty("actualGrossWeight")]
        public decimal ActualGrossWeight { get; set; }

        [Column("Actual_Tare_Weight")]
        [JsonProperty("actualTareWeight")]
        public decimal ActualTareWeight { get; set; }

        [Column("Actual_Net_Weight")]
        [JsonProperty("actualNetWeight")]
        public decimal ActualNetWeight { get; set; }
    }
}
