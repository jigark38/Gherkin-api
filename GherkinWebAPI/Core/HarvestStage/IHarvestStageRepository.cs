using GherkinWebAPI.Models.HarvestStage;
using GherkinWebAPI.Request.HarvestStage;
using GherkinWebAPI.Response.HarvestStage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.HarvestStage
{
    public interface IHarvestStageRepository
    {
        Task<HarvestStageResponse> InsertHarvestStages(HarvestStageInsertRequest rMStockInsertRequest);
        Task<List<EffectiveDateForHarvestDetails>> GetEffectiveDateList(string cropNameCode);
        Task<HarvestStageInsertRequest> GetHarvestStageDetails(string hsTransactionCode);
    }
}
