using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class ProductionScheduleDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [JsonProperty("productionScheduleNo")]
        public int Production_Schedule_No { get; set; }

        [JsonProperty("productionScheduleDate")]
        public DateTime Production_Schedule_Date { get; set; }

        [JsonProperty("employeeID")]
        public string Employee_ID { get; set; }

        [JsonProperty("psThroughDetails")]
        public string PS_Through_Details { get; set; }

        [JsonProperty("psRequireDateBy")]
        public DateTime PS_Require_Date_By { get; set; }

        [JsonProperty("orgOfficeNo")]
        public int Org_office_No { get; set; }
       
    }


    public class ProductionSchedule
    {
        public ProductionSchedule()
        {
            DirectProductionSchedule = new List<DirectProductionSchedule>();
            SalesProductionSchedule = new List<SalesProductionSchedule>();
        }

        [JsonProperty("productionScheduleNo")]
        public int Production_Schedule_No { get; set; }

        [JsonProperty("productionScheduleDate")]
        public DateTime Production_Schedule_Date { get; set; }

        [JsonProperty("employeeID")]
        public string Employee_ID { get; set; }

        [JsonProperty("psThroughDetails")]
        public string PS_Through_Details { get; set; }

        [JsonProperty("psRequireDateBy")]
        public DateTime PS_Require_Date_By { get; set; }

        [JsonProperty("orgOfficeNo")]
        public int Org_office_No { get; set; }

        [JsonProperty("directProductionSchedule")]
        public List<DirectProductionSchedule> DirectProductionSchedule { get; set; }

        [JsonProperty("salesProductionSchedule")]
        public List<SalesProductionSchedule> SalesProductionSchedule { get; set; }

    }
}