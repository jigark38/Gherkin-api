﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models
{
    public class AreaMaterialReceivedDetailsModel
    {
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       [Key]
       [JsonProperty("MRDetailsAGNo")]
          public int MR_Details_AG_No { get; set; }

       [JsonProperty("AreaMRNo")]
          public long Area_MR_No { get; set; }

       [JsonProperty("RawMaterialGroupCode")]
          public string Raw_Material_Group_Code { get; set; }

       [JsonProperty("RawMaterialDetailsCode")]
          public string Raw_Material_Details_Code { get; set; }

       [JsonProperty("RMMaterialTransferQty")]
          public decimal RM_Material_Transfer_Qty { get; set; }

       [JsonProperty("RMMaterialReceivedQuantity")]
          public decimal RM_Material_Received_Quantity { get; set; }

       [JsonProperty("MRShortfallDamageQty")]
            public decimal MR_Shortfall_Damage_Qty { get; set; }

        }
    }