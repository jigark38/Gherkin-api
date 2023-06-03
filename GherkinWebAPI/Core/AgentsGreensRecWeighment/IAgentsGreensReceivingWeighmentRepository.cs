using GherkinWebAPI.DTO;
using GherkinWebAPI.DTO.AgentsGreensRecWeighment;
using GherkinWebAPI.DTO.MaterialInward;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.AgentsGreensRecWeighment;
using GherkinWebAPI.Request.GreensAgentSupplierDetails;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.AgentsGreensRecWeighment
{
    public interface IAgentsGreensReceivingWeighmentRepository
    {
        Task<ApiResponse<List<OrganisationOfficeLocationDetailsDto>>> GetOrgOfficeLocation();
        Task<ApiResponse<List<MaterialInwardDto>>> GetInwardDetails(int officeOrgNumber);
        Task<ApiResponse<List<SupplierInformationDetailsRequest>>> GetSupplierInformationDetail();
        Task<ApiResponse<List<CropScheme>>> GetCropSchemeDetails(string cropnameCode);
        Task<ApiResponse<GreensAgentReceivedDetailsDTO>> PartialSaveGreensRecvDetails(GreensAgentReceivedDetails recvDetail);
        Task<ApiResponse<GreensAgentReceivedDetailsDTO>> GetGreensRecvDetails(string inwardGatepassNo);
        Task<ApiResponse<bool>> ChangeInGoingStatus(int GRNNo);
        Task<ApiResponse<GreensAgentReceivedDetailsDTO>> SaveRecvWeighmentDetails(GreensAgentReceivedDetails agentsGrnRecivWeigmtDetail);
    }
}
