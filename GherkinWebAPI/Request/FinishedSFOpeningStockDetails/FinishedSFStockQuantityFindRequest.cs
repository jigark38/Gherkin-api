﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Request.FinishedSFOpeningStockDetails
{
    public class FinishedSFStockQuantityFindRequest
    {
        [JsonProperty("orgOfficeNo")]
        public int OrgOfficeNo { get; set; }
        [JsonProperty("areaID")]
        public string AreaID { get; set; }
        [JsonProperty("fSFStockType")]
        public string FSFStockType { get; set; }
        [JsonProperty("fSFPackingMode")]
        public string FSFPackingMode { get; set; }
        [JsonProperty("cBCode")]
        public string CBCode { get; set; }
        [JsonProperty("profInvNo")]
        public string ProfInvNo { get; set; }
        [JsonProperty("fPGroupCode")]
        public string FPGroupCode { get; set; }
        [JsonProperty("fPVarietyCode")]
        public string FPVarietyCode { get; set; }
        [JsonProperty("productionProcessCode")]
        public string ProductionProcessCode { get; set; }
        [JsonProperty("mediaProcessCode")]
        public string MediaProcessCode { get; set; }
        [JsonProperty("fPGradeCode")]
        public string FPGradeCode { get; set; }
    }
}