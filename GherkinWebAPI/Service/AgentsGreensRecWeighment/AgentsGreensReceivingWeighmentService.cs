using GherkinWebAPI.Core;
using GherkinWebAPI.Core.AgentsGreensRecWeighment;
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
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.AgentsGreensRecWeighment
{
    public class AgentsGreensReceivingWeighmentService : IAgentsGreensReceivingWeighmentService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public AgentsGreensReceivingWeighmentService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<ApiResponse<List<OrganisationOfficeLocationDetailsDto>>> GetOrgOfficeLocation()
        {
            return await _repositoryWrapper.AgentsGreensReceivingWeighmentRepository.GetOrgOfficeLocation();
        }
        public async Task<ApiResponse<List<MaterialInwardDto>>> GetInwardDetails(int officeOrgNumber)
        {
            return await _repositoryWrapper.AgentsGreensReceivingWeighmentRepository.GetInwardDetails(officeOrgNumber);
        }

        public async Task<ApiResponse<List<SupplierInformationDetailsRequest>>> GetSupplierInformationDetail()
        {
            return await _repositoryWrapper.AgentsGreensReceivingWeighmentRepository.GetSupplierInformationDetail();
        }

        public async Task<ApiResponse<List<CropScheme>>> GetCropSchemeDetails(string cropnameCode)
        {
            return await _repositoryWrapper.AgentsGreensReceivingWeighmentRepository.GetCropSchemeDetails(cropnameCode);
        }

        public async Task<ApiResponse<GreensAgentReceivedDetailsDTO>> PartialSaveGreensRecvDetails(GreensAgentReceivedDetails recvDetail)
        {
            return await _repositoryWrapper.AgentsGreensReceivingWeighmentRepository.PartialSaveGreensRecvDetails(recvDetail);
        }

        public async Task<ApiResponse<GreensAgentReceivedDetailsDTO>> GetGreensRecvDetails(string inwardGatepassNo)
        {
            return await _repositoryWrapper.AgentsGreensReceivingWeighmentRepository.GetGreensRecvDetails(inwardGatepassNo);
        }        

        public async Task<ApiResponse<bool>> ChangeInGoingStatus(int GRNNo)
        {
            return await _repositoryWrapper.AgentsGreensReceivingWeighmentRepository.ChangeInGoingStatus(GRNNo);
        }
        public async Task<ApiResponse<GreensAgentReceivedDetailsDTO>> SaveRecvWeighmentDetails(GreensAgentReceivedDetails agentsGrnRecivWeigmtDetail)
        {
            return await _repositoryWrapper.AgentsGreensReceivingWeighmentRepository.SaveRecvWeighmentDetails(agentsGrnRecivWeigmtDetail);
        }
    }
}