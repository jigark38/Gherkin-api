using GherkinWebAPI.DTO.BranchIndent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Response.BranchIndent
{
    public class BranchIndentResponse
    {
        public BranchIndentResponse()
        {
            BranchIndentDetails = new BranchIndentDetailsDto();
            BranchIndentMaterialDetails = new List<BranchIndentMaterialDetailsDto>();
        }
        public BranchIndentDetailsDto BranchIndentDetails { get; set; }
        public List<BranchIndentMaterialDetailsDto> BranchIndentMaterialDetails { get; set; }
    }
}