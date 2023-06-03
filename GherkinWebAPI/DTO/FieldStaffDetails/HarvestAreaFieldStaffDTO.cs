using GherkinWebAPI.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO
{
    public class HarvestAreaFieldStaffDTO
    {
        [JsonProperty("dateOfEntry")]
        public DateTime EntryDate { get; set; }
        [JsonProperty("loginUserName")]
        public string LoginUserName { get; set; }
        [JsonProperty("areaID")]
        public string AreaID { get; set; }
        [JsonProperty("areaCode")]
        public int? AreaCode { get; set; }
        [JsonProperty("fieldStaffs")]
        public ICollection<FieldStaffDTO> FieldStaffs { get; set; }
    }
}