using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service
{
    public class FarmersAgreementService : IFarmersAgreementService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public FarmersAgreementService(IRepositoryWrapper repositoryWrapper)
        {
            this._repositoryWrapper = repositoryWrapper;
        }

        public async Task<FarmersAgreementDetail> CreateAgreement(FarmersAgreementDetail farmersAgreement)
        {
            return await _repositoryWrapper.FarmersAgreementRepository.CreateAgreement(farmersAgreement);
        }

        public async Task<FarmersAgreement> SearchAgreement(string areaId, int cityCode, string farmersCode, string cropGroupCode, string cropNameCode, string psNumber)
        {
            return await _repositoryWrapper.FarmersAgreementRepository.SearchAgreement(areaId, cityCode, farmersCode, cropGroupCode, cropNameCode, psNumber); ;
        }

        public async Task<string> GetFarmersAgreementCodeAsync()
        {
            return await _repositoryWrapper.FarmersAgreementRepository.GetFarmersAgreementCodeAsync();
        }

        public async Task<ApiResponse<object>> UpdateAgreement(string farmersAgreementCode, FarmersAgreement farmersAgreement)
        {
            return await _repositoryWrapper.FarmersAgreementRepository.UpdateAgreement(farmersAgreementCode, farmersAgreement);
        }
        public async Task<FieldStaffDetails> GetAreaInchargeDetailsByAreaId(string areaId, DateTime date)
        {
            
            return  await _repositoryWrapper.FarmersAgreementRepository.GetAreaInchargeDetailsByAreaId(areaId, date);
        }

        public async Task<bool> ValidateFarmerAccount(string FarmersAccountNo, string FarmerCode, string PSNumber)
        {
            return await _repositoryWrapper.FarmersAgreementRepository.ValidateFarmerAccount(FarmersAccountNo, FarmerCode, PSNumber);
        }
        public async Task<List<string>> FarmerAccountList(string CropGroupCode, string CropNameCode, string PSNumber)
        {
            return await _repositoryWrapper.FarmersAgreementRepository.FarmerAccountList(CropGroupCode, CropNameCode, PSNumber);
        }
        public async Task<FarmersAgreement> FarmerDetailsByAccount(string FarmersAccountNo, string PSNumber)
        {
            return await _repositoryWrapper.FarmersAgreementRepository.FarmerDetailsByAccount(FarmersAccountNo, PSNumber);
        }
    }
}