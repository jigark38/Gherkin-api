using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO
{
    public class ProformaInvoiceDto
    {
        [JsonProperty("profInvNo")]
        public string Prof_Inv_No { get; set; }
        [JsonProperty("profInvoiceAmount")]
        public decimal Prof_Invoice_Amount { get; set; }
        [JsonProperty("consigneeCbCode")]
        public string C_B_Code { get; set; }
    }
}