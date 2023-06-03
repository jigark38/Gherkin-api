using GherkinWebAPI.Models.HarvestStage;
using GherkinWebAPI.Request.HarvestStage;
using GherkinWebAPI.Response;
using GherkinWebAPI.Response.HarvestStage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.HarvestStage
{
    public interface IHarvestStageService
    {
        Task<HarvestStageShowResponse> GetHarvestStageFormDataAsync();
        Task<HarvestStageResponse> InsertHarvestStages(HarvestStageInsertRequest harvestStageInsertRequest);
        Task<List<EffectiveDateForHarvestDetails>> GetEffectiveDateList(string cropNameCode);
        Task<HarvestStageInsertRequest> GetHarvestStageDetails(string hsTransactionCode);
    }
}