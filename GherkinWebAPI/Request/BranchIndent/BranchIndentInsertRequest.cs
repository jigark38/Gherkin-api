using GherkinWebAPI.Models.Branch_Indent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Request.BranchIndent
{
    public class BranchIndentInsertRequest
    {
        public BranchIndentDetailsModel BranchIndentDetails { get; set; }

        public List<BranchIndentMaterialDetailsModel> BranchIndentMaterialDetails { get; set; }
    }
}