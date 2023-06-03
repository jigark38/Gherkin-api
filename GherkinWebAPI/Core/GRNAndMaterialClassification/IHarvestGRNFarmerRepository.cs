using GherkinWebAPI.Models.GRNAndMaterialClassification;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.GRNAndMaterialClassification
{
    public interface IHarvestGRNFarmerRepository
    {
        Task<HarvestGRNMaterialDetail> CreateHarvestGRNFarmer(HarvestGRNMaterialDetail materialDetail);
    }
}
