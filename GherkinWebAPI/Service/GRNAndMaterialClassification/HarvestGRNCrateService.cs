using GherkinWebAPI.Core;
using GherkinWebAPI.Core.GRNAndMaterialClassification;
using GherkinWebAPI.Models.GRNAndMaterialClassification;
using System.Threading.Tasks;

namespace GherkinWebAPI.Service.GRNAndMaterialClassification
{
    public class HarvestGRNCrateService : IHarvestGRNCrateService
    {
        private readonly IRepositoryWrapper _repository;

        public HarvestGRNCrateService(IRepositoryWrapper repository)
        {
            _repository = repository;
        }
        public async Task<HarvestGRNMaterialDetail> CreateHarvestGRNCrate(HarvestGRNMaterialDetail materialDetail)
        {
            return await _repository.HarvestGRNCrateRepository.CreateHarvestGRNCrate(materialDetail);
        }
    }
}