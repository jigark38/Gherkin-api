using GherkinWebAPI.Request.SowingFarming;
using GherkinWebAPI.Response.SowingFarming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.SowingFarming
{
    public interface ISowingFarmingRepository
    {
        Task<SowingFarmingInsert> InserSowingFarmingDetails(SowingFarmingInsert sowingFarmingInsert);
        Task<SowingFarmingDataForFormRequiredForGrid> GetSowingFarmingDataForFormRequiredForGrid(DateTime sowingDate, string cropNameCode, string psNumber);
    }
}
