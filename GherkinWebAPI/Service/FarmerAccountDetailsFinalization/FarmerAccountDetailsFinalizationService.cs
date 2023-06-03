using GherkinWebAPI.Core;
using GherkinWebAPI.Core.FarmerAccountDetailsFinalization;
using GherkinWebAPI.DTO.FarmersAccountSettlementDetail;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.FarmerAccountDetailsFinalization;
using GherkinWebAPI.Response;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.FarmerAccountDetailsFinalization
{

    public class FarmerAccountDetailsFinalizationService : IFarmerAccountDetailsFinalizationService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public FarmerAccountDetailsFinalizationService(IRepositoryWrapper repositoryWrapper)
        {
            this._repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<FarmerNameAccountVM>> SearchAgreement(FarmerAccountSettlementSearchDTO settlementSearchDTO)
        {
            return await _repositoryWrapper.FarmerAccountDetailsFinalizationRepository.SearchAgreement(settlementSearchDTO);
        }

        public async Task<ApiResponse<object>> CreateAgreement(FarmersAccountSettlementDetail farmersAccountSettlementDetail)
        {
            return await _repositoryWrapper.FarmerAccountDetailsFinalizationRepository.CreateAgreement(farmersAccountSettlementDetail);
        }
        public async Task<List<GreensReceivingDetailsVM>> GetSettlementDetails(FarmerAccountSettlementSearchDTO settlementSearchDTO)
        {
            return await _repositoryWrapper.FarmerAccountDetailsFinalizationRepository.GetSettlementDetails(settlementSearchDTO);
        }

        public async Task<List<FarmerAdvancesPaidGRID>> GetFarmerAdvanceDetails(FarmerAccountSettlementSearchDTO farmerDetailsFilterModel)
        {
            return await _repositoryWrapper.FarmerAccountDetailsFinalizationRepository.GetFarmerAdvanceDetails(farmerDetailsFilterModel);

        }

        public async Task<List<FarmerInputReturnGRID>> GetFarmerInputsReturnDetails(FarmerAccountSettlementSearchDTO settlementSearchDTO)
        {
            return await _repositoryWrapper.FarmerAccountDetailsFinalizationRepository.GetFarmerInputsReturnDetails(settlementSearchDTO);
        }

        public async Task<List<FarmerInputsIssuedGRID>> GetFarmerInputsIssuedDetails(FarmerAccountSettlementSearchDTO settlementSearchDTO)
        {
            return await _repositoryWrapper.FarmerAccountDetailsFinalizationRepository.GetFarmerInputsIssuedDetails(settlementSearchDTO);

        }

        public async Task<List<FarmerAgreementAndSettlementInfo>> GetFarmerAgreementAndSettlementInfo(FarmerAccountSettlementSearchDTO settlementSearchDTO)
        {
            return await _repositoryWrapper.FarmerAccountDetailsFinalizationRepository.GetFarmerAgreementAndSettlementInfo(settlementSearchDTO);

        }

        public async Task<List<FarmerGreenReceivingGRID>> GetFarmerGreensReveivingDetails(FarmerAccountSettlementSearchDTO settlementSearchDTO)
        {
            return await _repositoryWrapper.FarmerAccountDetailsFinalizationRepository.GetFarmerGreensReveivingDetails(settlementSearchDTO);

        }

        public async Task<List<InputIssue>> GetFarmerInputIssues(FarmerAccountSettlementSearchDTO settlementSearchDTO)
        {
            return await _repositoryWrapper.FarmerAccountDetailsFinalizationRepository.GetFarmerInputIssues(settlementSearchDTO);
        }

        public async Task<List<InputReturn>> GetFarmerInputReturns(FarmerAccountSettlementSearchDTO settlementSearchDTO)
        {
            return await _repositoryWrapper.FarmerAccountDetailsFinalizationRepository.GetFarmerInputReturns(settlementSearchDTO);
        }
    }
}