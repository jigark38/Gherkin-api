using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO
{
    public class ConsigneeBuyersDto
    {
        [JsonProperty("cbCode")]
        public string C_B_Code { get; set; }
        [JsonProperty("cbName")]
        public string C_B_Name { get; set; }
        [JsonProperty("countryId")]
        public string W_Country_Id { get; set; }
    }
}