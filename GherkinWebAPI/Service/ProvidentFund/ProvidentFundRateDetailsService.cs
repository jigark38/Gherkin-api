using GherkinWebAPI.Core;
using GherkinWebAPI.Core.ProvidentFund;
using GherkinWebAPI.DTO.ProvidentFund;
using GherkinWebAPI.Models.ProvidentFund;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.ProvidentFund
{
    public class ProvidentFundRateDetailsService : IProvidentFundRateDetailsService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public ProvidentFundRateDetailsService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<Models.ProvidentFund.ProvidentFundRateDetails> AddProvidentFundRateDetail(Models.ProvidentFund.ProvidentFundRateDetails providentFundRateDetail)
        {
            return await _repositoryWrapper.ProvidentFundRateDetailsRepository.AddProvidentFundRateDetail(providentFundRateDetail);
        }

        public async Task<List<ProvidentFundRateDetails>> GetProvidentFundRateDetails()
        {
            return await _repositoryWrapper.ProvidentFundRateDetailsRepository.GetProvidentFundRateDetails();
        }

        public async Task<Models.ProvidentFund.ProvidentFundRateDetails> UpdateProvidentFundRateDetail(Models.ProvidentFund.ProvidentFundRateDetails providentFundRateDetail)
        {
            return await _repositoryWrapper.ProvidentFundRateDetailsRepository.UpdateProvidentFundRateDetail(providentFundRateDetail);
        }
    }
}