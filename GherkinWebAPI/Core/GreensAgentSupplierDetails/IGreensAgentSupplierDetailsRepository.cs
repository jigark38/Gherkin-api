using GherkinWebAPI.Models.GreensAgentSupplierDetails;
using GherkinWebAPI.Request.GreensAgentSupplierDetails;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.GreensAgentSupplierDetails
{
    public interface IGreensAgentSupplierDetailsRepository
    {
        Task<ApiResponse<object>> SaveGreensAgentSupplierDetails(SupplierInformationDetailsRequest supplierInformationDetail);
        Task<ApiResponse<List<AgentOrgDetailsRequest>>> GetAgentOrganisationDetails();
        Task<ApiResponse<SupplierInformationDetailsRequest>> GetSupplierInformationDetail(int AgentOrgID);
        Task<ApiResponse<object>> SaveBankAccountDetails(List<AgentBankDetailsRequest> agntBankDetailList);
        Task<ApiResponse<AgentOrgDocuments>> GetDocumentByDocId(int docId);
        Task<ApiResponse<object>> DeleteDocumentByDocId(int docId);
        Task<ApiResponse<object>> ModifyGreensAgentSupplierDetails(SupplierInformationDetailsRequest suppInfoReq);
    }
}
