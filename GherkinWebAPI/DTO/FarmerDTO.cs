using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO
{
    public class FarmerDocumentDTO
    {
        [JsonProperty("farmerCode")]
        public string Farmer_Code { get; set; }
        [JsonProperty("documentName")]
        public string DocumentName { get; set; }

    }
}
