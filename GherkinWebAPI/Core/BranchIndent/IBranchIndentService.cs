using GherkinWebAPI.DTO.BranchIndent;
using GherkinWebAPI.Models.Branch_Indent;
using GherkinWebAPI.Request.BranchIndent;
using GherkinWebAPI.Response.BranchIndent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.BranchIndent
{
    public interface IBranchIndentService
    {
        Task<BranchIndentResponse> InsertBranchIndentDetails(BranchIndentInsertRequest branchIndentInsertRequest);
        Task<List<BranchIndentMaterialDetailsDto>> GetRMUOM();
        Task<List<BranchIndentResponse>> GetAllIndentRequest();
        Task<bool> UpdateIndentMaterial(BranchIndentMaterialDetailsModel branchIndentMaterialDetailsModel);
    }
}
