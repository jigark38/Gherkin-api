using GherkinWebAPI.DTO.ProvidentFund;
using GherkinWebAPI.Models.ProvidentFund;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.ProvidentFund
{
    public interface IProvidentFundRateDetailsRepository
    {
        Task<ProvidentFundRateDetails> AddProvidentFundRateDetail(ProvidentFundRateDetails providentFundRateDetail);
        Task<ProvidentFundRateDetails> UpdateProvidentFundRateDetail(ProvidentFundRateDetails providentFundRateDetail);
        Task<List<ProvidentFundRateDetails>> GetProvidentFundRateDetails();
    }
}
