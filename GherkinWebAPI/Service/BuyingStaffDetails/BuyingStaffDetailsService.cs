using GherkinWebAPI.Core;
using GherkinWebAPI.Core.BuyingStaffDetails;
using GherkinWebAPI.Models;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.BuyingStaffDetails
{
    public class BuyingStaffDetailsService : IBuyingStaffDetailsService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public BuyingStaffDetailsService(IRepositoryWrapper repositoryWrapper)
        {
            this._repositoryWrapper = repositoryWrapper;
        }

        public async Task<ApiResponse<object>> AddBuyingStaffDetails(List<HarvestAreaBuyingStaffDetails> harvestAreaBuyingStaffDetailsList)
        {
            return await _repositoryWrapper.BuyingStaffDetailsRepository.AddBuyingStaffDetails(harvestAreaBuyingStaffDetailsList);
        }

        public async Task<ApiResponse<object>> UpdateBuyingStaffDetails(List<HarvestAreaBuyingStaffDetails> harvestAreaBuyingStaffDetailsList)
        {
            return await _repositoryWrapper.BuyingStaffDetailsRepository.UpdateBuyingStaffDetails(harvestAreaBuyingStaffDetailsList);
        }

        public async Task<ApiResponse<List<HarvestAreaBuyingStaffDetails>>> getBuyingStaffDetailsByEmployee(string employeId)
        {
            return await _repositoryWrapper.BuyingStaffDetailsRepository.getBuyingStaffDetailsByEmployee(employeId);
        }
        public async Task<ApiResponse<List<HarvestAreaBuyingStaffDetails>>> DeleteBuyingStaffDetailsByEmployee(string employeId, string areaId)
        {
            return await _repositoryWrapper.BuyingStaffDetailsRepository.DeleteBuyingStaffDetailsByEmployee(employeId, areaId);
        }
    }
}