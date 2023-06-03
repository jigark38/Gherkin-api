using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO
{
    public class OrganisationOfficeUnitDto
    {
        [JsonProperty("orgOfficeNo")]
        public int Org_Office_No { get; set; }

        [JsonProperty("orgCode")]
        public int Org_Code { get; set; }

        [JsonProperty("orgOfficeName")]
        public string Org_Office_Name { get; set; }
    }
}