﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace GherkinWebAPI.Models
{
     public class AreaMRInwardPostDetails
    {
        [Key]
        [JsonProperty("AreaMRNo")]
       public long Area_MR_No { get; set; }

        [JsonProperty("AreaID")]
       public string Area_ID { get; set; }

        [JsonProperty("RMTransferNo")]
       public string RM_Transfer_No { get; set; }

        [JsonProperty("OGPNO")]
       public string OGP_NO { get; set; }

        [JsonProperty("CropSchemeCode")]
       public string Crop_Scheme_Code { get; set; }

        [JsonProperty("PSNumber")]
       public string PS_Number { get; set; }

        [JsonProperty("AreaMRDate")]
       public DateTime Area_MR_Date { get; set; }

        [JsonProperty("AreaMRNNoVisible")]
       public string Area_MRN_No_Visible { get; set; }

        [JsonProperty("MRExpensesPaid")]
       public int MR_Expenses_Paid { get; set; }

        [JsonProperty("MRRemarks")]
        public string MR_Remarks { get; set; }

        public List<AreaMaterialReceivedDetailsModel> AreaMaterialReceivedDetails { get; set; }
    }


    public class AreaMRInwardDetails
    {
        [Key]
        [JsonProperty("AreaMRNo")]
        public long Area_MR_No { get; set; }

        [JsonProperty("AreaID")]
        public string Area_ID { get; set; }

        [JsonProperty("RMTransferNo")]
        public string RM_Transfer_No { get; set; }

        [JsonProperty("OGPNO")]
        public string OGP_NO { get; set; }

        [JsonProperty("CropSchemeCode")]
        public string Crop_Scheme_Code { get; set; }

        [JsonProperty("PSNumber")]
        public string PS_Number { get; set; }

        [JsonProperty("AreaMRDate")]
        public DateTime Area_MR_Date { get; set; }

        [JsonProperty("AreaMRNNoVisible")]
        public string Area_MRN_No_Visible { get; set; }

        [JsonProperty("MRExpensesPaid")]
        public int MR_Expenses_Paid { get; set; }

        [JsonProperty("MRRemarks")]
        public string MR_Remarks { get; set; }
    }
}