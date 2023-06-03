using GherkinWebAPI.Models.GRNAndMaterialClassification;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.GRNAndMaterialClassification
{
    public interface IHarvestGRNFarmerService
    {
        Task<HarvestGRNMaterialDetail> CreateHarvestGRNFarmer(HarvestGRNMaterialDetail materialDetail);
    }
}
