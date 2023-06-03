﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO
{
    public class AreaMaterialReceivedDetailsDTO
    {
          //[JsonProperty("")]
       public int MR_Details_AG_No { get; set; }

       public long Area_MR_No { get; set; }

       public string Raw_Material_Group_Code { get; set; }

       public string Raw_Material_Details_Code { get; set; }

       public decimal RM_Material_Transfer_Qty { get; set; }

       public decimal RM_Material_Received_Quantity { get; set; }

       public decimal MR_Shortfall_Damage_Qty { get; set; }
    }
}