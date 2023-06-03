using GherkinWebAPI.Request.SowingFarming;
using GherkinWebAPI.Response.SowingFarming;
using System;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.SowingFarming
{
    public interface ISowingFarmingService
    {
        Task<SowingFarmingFormDataResponse> GetSowingFarmingDataForFormByAreaId(string areaId);

        Task<SowingFarmingInsert> InsertSowingFamring(SowingFarmingInsert sowingFarmingInsert);
        Task<SowingFarmingDataForFormRequiredForGrid> GetSowingFarmingDataForFormRequiredForGrid(DateTime sowingDate, string cropNameCode, string psNumber);
    }

}
