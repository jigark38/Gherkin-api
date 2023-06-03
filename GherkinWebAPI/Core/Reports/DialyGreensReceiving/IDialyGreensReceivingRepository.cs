using GherkinWebAPI.DTO.Reports.DialyGreensReceiving;
using GherkinWebAPI.Models.DailyHarvestDetails;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Core.Reports.DialyGreensReceiving
{
    public interface IDialyGreensReceivingRepository
    {
        Task<ApiResponse<List<DialyGreensReceivingForBuyersDto>>> GetBuyers(DateTime date, string areaCode);

        Task<ApiResponse<object>> GetDailyBuyerWiseReport(DialyGreensReceivingForBuyersDto data);
       
    }
}