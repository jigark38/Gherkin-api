using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.GRPAndroid.ProdProcessBOM
{
    public class ProductionProcessDetailsModel
    {
        [JsonProperty("productionProcessCode")]

        public string ProductionProcessCode { get; set; }

        [JsonProperty("dateofCreation")]
        public DateTime DateofCreation { get; set; }

        [JsonProperty("employeeID")]

        public string EmployeeID { get; set; }

        [JsonProperty("fpGroupCode")]

        public string FPGroupCode { get; set; }

        [JsonProperty("fpVarietyCode")]

        public string FPVarietyCode { get; set; }

        [JsonProperty("productionProcessName")]

        public string ProductionProcessName { get; set; }

        [JsonProperty("productionProcessDescription")]

        public string ProductionProcessDescription { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }

    }
}