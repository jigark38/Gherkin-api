using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class HarvestGRNInwardDetails
    {
        [Key]
        public long Unit_HM_Inward_No { get; set; }
        public string Inward_Gate_Pass_No { get; set; }
        public int Org_office_No { get; set;}
        public string Area_ID { get; set; }
        public long Harvest_GRN_No { get; set; }
        public string Employee_ID { get; set; }
        public int Harvest_GRN_Total_Desp_Crates { get; set; }
        public decimal Harvest_GRN_Total_Quantity { get; set; }
        public DateTime Unit_Harvest_Material_Inward_Date { get; set; }
        public int Vehicle_Reach_Reading { get; set; }
        public TimeSpan Vehicle_Reach_time { get; set; }
        public decimal Vehicle_Transit_Duration { get; set; }
        public int Vehicle_Transit_Kms { get; set; }
        public int Total_Received_Crates { get; set; }
        public decimal Total_Received_Qty { get; set; }
        public int? Greens_Procurement_No { get; set; }
    }
}