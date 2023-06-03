using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using GherkinWebAPI.Models;
using GherkinWebAPI.DTO.FarmersAccountSettlementDetail;
using GherkinWebAPI.Models.FarmerAccountDetailsFinalization;
using GherkinWebAPI.Utilities;
using GherkinWebAPI.Response;

namespace GherkinWebAPI.Core.FarmerAccountDetailsFinalization
{
    public interface IFarmerAccountDetailsFinalizationRepository
    {
        Task<List<FarmerNameAccountVM>> SearchAgreement(FarmerAccountSettlementSearchDTO settlementSearchDTO);
        Task<List<GreensReceivingDetailsVM>> GetSettlementDetails(FarmerAccountSettlementSearchDTO settlementSearchDTO);
        Task<ApiResponse<object>> CreateAgreement(FarmersAccountSettlementDetail farmersAccountSettlementDetail);
        Task<List<FarmerAdvancesPaidGRID>> GetFarmerAdvanceDetails(FarmerAccountSettlementSearchDTO farmerDetailsFilterModel);
        Task<List<FarmerInputReturnGRID>> GetFarmerInputsReturnDetails(FarmerAccountSettlementSearchDTO settlementSearchDTO);
        Task<List<FarmerInputsIssuedGRID>> GetFarmerInputsIssuedDetails(FarmerAccountSettlementSearchDTO settlementSearchDTO);
        Task<List<FarmerAgreementAndSettlementInfo>> GetFarmerAgreementAndSettlementInfo(FarmerAccountSettlementSearchDTO settlementSearchDTO);
        Task<List<FarmerGreenReceivingGRID>> GetFarmerGreensReveivingDetails(FarmerAccountSettlementSearchDTO settlementSearchDTO);

        Task<List<InputIssue>> GetFarmerInputIssues(FarmerAccountSettlementSearchDTO settlementSearchDTO);
        Task<List<InputReturn>> GetFarmerInputReturns(FarmerAccountSettlementSearchDTO settlementSearchDTO);
    }
}