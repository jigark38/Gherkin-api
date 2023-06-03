using GherkinWebAPI.Models.GRNAndMaterialClassification;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.GRNAndMaterialClassification
{
    public interface IHarvestGRNCrateRepository
    {
        Task<HarvestGRNMaterialDetail> CreateHarvestGRNCrate(HarvestGRNMaterialDetail materialDetail);
    }
}
