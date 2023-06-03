using GherkinWebAPI.Core;
using GherkinWebAPI.Core.GreensAgentSupplierDetails;
using GherkinWebAPI.Models.GreensAgentSupplierDetails;
using GherkinWebAPI.Request.GreensAgentSupplierDetails;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.GreensAgentSupplierDetails
{
    public class GreensAgentSupplierDetailsService : IGreensAgentSupplierDetailsService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public GreensAgentSupplierDetailsService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<ApiResponse<object>> SaveGreensAgentSupplierDetails(SupplierInformationDetailsRequest supplierInformationDetail)
        {
            return await _repositoryWrapper.GreensAgentSupplierDetailsRepository.SaveGreensAgentSupplierDetails(supplierInformationDetail);
        }

        public async Task<ApiResponse<List<AgentOrgDetailsRequest>>> GetAgentOrganisationDetails()
        {
            return await _repositoryWrapper.GreensAgentSupplierDetailsRepository.GetAgentOrganisationDetails();
        }

        public async Task<ApiResponse<SupplierInformationDetailsRequest>> GetSupplierInformationDetail(int AgentOrgID)
        {
            return await _repositoryWrapper.GreensAgentSupplierDetailsRepository.GetSupplierInformationDetail(AgentOrgID);
        }

        public async Task<ApiResponse<object>> SaveBankAccountDetails(List<AgentBankDetailsRequest> agntBankDetailList)
        {
            return await _repositoryWrapper.GreensAgentSupplierDetailsRepository.SaveBankAccountDetails(agntBankDetailList);
        }

        public async Task<ApiResponse<AgentOrgDocuments>> GetDocumentByDocId(int docId)
        {
            return await _repositoryWrapper.GreensAgentSupplierDetailsRepository.GetDocumentByDocId(docId);
        }
        public async Task<ApiResponse<object>> DeleteDocumentByDocId(int docId)
        {
            return await _repositoryWrapper.GreensAgentSupplierDetailsRepository.DeleteDocumentByDocId(docId);
        }

        public async Task<ApiResponse<object>> ModifyGreensAgentSupplierDetails(SupplierInformationDetailsRequest suppInfoReq)
        {
            return await _repositoryWrapper.GreensAgentSupplierDetailsRepository.ModifyGreensAgentSupplierDetails(suppInfoReq);
        }
    }
}