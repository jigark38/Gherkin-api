using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.ProformaInvoiceDetail
{
    public class ProductDetailsDto
    {
        [JsonProperty("profInvDate")]
        public DateTime Prof_Inv_Date { get; set; }

        [JsonProperty("profInvNo")]
        public string Prof_Inv_No { get; set; }

        [JsonProperty("profInvProdNo")]
        public string Prof_Inv_Prod_No { get; set; }

        [JsonProperty("consigneeCbCode")]
        public string C_B_Code { get; set; }

        [JsonProperty("consigneeCbName")]
        public string C_B_Name { get; set; }

        [JsonProperty("CountryId")]
        public string W_Country_Id { get; set; }

        [JsonProperty("CountryName")]
        public string W_Country_Name { get; set; }
  
        [JsonProperty("fPGroupCode")]
        public string FP_Group_Code { get; set; }

        [JsonProperty("fPVarietyCode")]
        public string FP_Variety_Code { get; set; }

        [JsonProperty("fPVarietyName")]
        public string FP_Variety_Name { get; set; }

        [JsonProperty("fPGradeCode")]
        public string FP_Grade_Code { get; set; }

        [JsonProperty("fPGrade")]
        public string FP_Grade { get; set; }

        [JsonProperty("preservedIn")]
        public string Preserved_In { get; set; }

        [JsonProperty("packUOM")]
        public string Packing_UOM { get; set; }

        [JsonProperty("qtyDrum")]
        public decimal Qty_Drum { get; set; }
    }
}