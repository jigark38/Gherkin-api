using GherkinWebAPI.Models.GRNAndMaterialClassification;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.GRNAndMaterialClassification
{
    public interface IHarvestGRNCrateService
    {
        Task<HarvestGRNMaterialDetail> CreateHarvestGRNCrate(HarvestGRNMaterialDetail materialDetail);
    }
}
