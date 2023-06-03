using GherkinWebAPI.Core.BranchIndent;
using GherkinWebAPI.DTO.BranchIndent;
using GherkinWebAPI.Models.Branch_Indent;
using GherkinWebAPI.Request.BranchIndent;
using GherkinWebAPI.Response.BranchIndent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.BranchIndent
{
    public class BranchIndentService : IBranchIndentService
    {
        private readonly IBranchIndentRepository branchIndentRepository;

        public BranchIndentService(IBranchIndentRepository branchIndentRepository)
        {
            this.branchIndentRepository = branchIndentRepository;
        }
        public async Task<BranchIndentResponse> InsertBranchIndentDetails(BranchIndentInsertRequest branchIndentInsertRequest)
        {
            BranchIndentResponse branchIndentResponse = await branchIndentRepository.InsertBranchIndentDetails(branchIndentInsertRequest);
            return branchIndentResponse;
        }

        public async Task<List<BranchIndentMaterialDetailsDto>> GetRMUOM()
        {
            return await branchIndentRepository.GetRMUOM();
        }

        public async Task<List<BranchIndentResponse>> GetAllIndentRequest()
        {
            return await branchIndentRepository.GetAllIndentRequest();

        }

        public async Task<bool> UpdateIndentMaterial(BranchIndentMaterialDetailsModel branchIndentMaterialDetailsModel)
        {
            return await branchIndentRepository.UpdateIndentMaterial(branchIndentMaterialDetailsModel);
        }
    }
}