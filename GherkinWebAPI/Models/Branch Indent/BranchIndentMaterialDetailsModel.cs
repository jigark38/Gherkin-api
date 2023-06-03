﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.Branch_Indent
{
    public class BranchIndentMaterialDetailsModel
    {

        public int ID { get; set; }

        public string RM_Indent_No { get; set; }

        public string Raw_Material_Group_Code { get; set; }

        public string Raw_Material_Details_Code { get; set; }

        public decimal RM_Stock_On_Date { get; set; }

        public decimal RM_Indent_Req_Qty { get; set; }

        public DateTime RM_Require_Date { get; set; }

        public string RM_Remarks { get; set; }

        public string RM_UOM { get; set; }


    }
}