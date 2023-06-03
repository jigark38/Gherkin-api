using GherkinWebAPI.Models;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.BuyingStaffDetails
{
    public interface IBuyingStaffDetailsService
    {
        Task<ApiResponse<object>> AddBuyingStaffDetails(List<HarvestAreaBuyingStaffDetails> harvestAreaBuyingStaffDetailsList);
        Task<ApiResponse<object>> UpdateBuyingStaffDetails(List<HarvestAreaBuyingStaffDetails> harvestAreaBuyingStaffDetailsList);
        Task<ApiResponse<List<HarvestAreaBuyingStaffDetails>>> getBuyingStaffDetailsByEmployee(string employeId);
        Task<ApiResponse<List<HarvestAreaBuyingStaffDetails>>> DeleteBuyingStaffDetailsByEmployee(string employeId, string areaId);
    }
}
