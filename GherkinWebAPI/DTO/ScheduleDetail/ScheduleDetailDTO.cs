using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.ScheduleDetail
{
    public class ScheduleDetailDTO
    {
        [JsonProperty("mediaProcessName")]

        public string MediaProcessName { get; set; }

        [JsonProperty("batchProductionDate")]

        public DateTime BatchProductionDate { get; set; }

        [JsonProperty("batchProductionNo")]

        public long BatchProductionNo { get; set; }

        [JsonProperty("bSOrderProductionNo")]

        public long BS_Order_Production_No { get; set; }


        [JsonProperty("pSSalesOrderScheduleNo")]

        public long PS_Sales_Order_Schedule_No { get; set; }


        [JsonProperty("pSDirectOrderScheduleNo")]

        public long PS_Direct_Order_Schedule_No { get; set; }


        [JsonProperty("fPGroupCode")]

        public string FP_Group_Code { get; set; }


        [JsonProperty("fPVarietyCode")]

        public string FP_Variety_Code { get; set; }


        [JsonProperty("fPGradeCode")]

        public string FP_Grade_Code { get; set; }


        [JsonProperty("packUOM")]

        public string Pack_UOM { get; set; }



        [JsonProperty("bSProductionQtyinUOM")]

        public long BS_Production_Qty_in_UOM { get; set; }

        [JsonProperty("fPVarietyName")]

        public string FP_Variety_Name { get; set; }

        [JsonProperty("gradeFromTo")]

        public string GradeFromTo { get; set; }

       

        

        [JsonProperty("productionScheduleNo")]

        public long Production_Schedule_No { get; set; }

        [JsonProperty("pSRequireDateBy")]

        public DateTime PS_Require_Date_By { get; set; }




    }
}