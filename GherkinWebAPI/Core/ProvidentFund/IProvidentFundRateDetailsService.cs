using GherkinWebAPI.DTO.ProvidentFund;
using GherkinWebAPI.Models.ProvidentFund;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.ProvidentFund
{
    public interface IProvidentFundRateDetailsService
    {
        Task<ProvidentFundRateDetails> AddProvidentFundRateDetail(ProvidentFundRateDetails addProvidentFundRateDetails);
        Task<ProvidentFundRateDetails> UpdateProvidentFundRateDetail(ProvidentFundRateDetails providentFundRateDetails);
        Task<List<ProvidentFundRateDetails>> GetProvidentFundRateDetails();
    }
}
