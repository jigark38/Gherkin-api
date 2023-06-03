using GherkinWebAPI.Core;
using GherkinWebAPI.Core.GRNAndMaterialClassification;
using GherkinWebAPI.Models.GRNAndMaterialClassification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.GRNAndMaterialClassification
{
    public class HarvestGRNFarmerService : IHarvestGRNFarmerService
    {
        private readonly IRepositoryWrapper _repository;

        public HarvestGRNFarmerService(IRepositoryWrapper repository)
        {
            _repository = repository;
        }
        public async Task<HarvestGRNMaterialDetail> CreateHarvestGRNFarmer(HarvestGRNMaterialDetail materialDetail)
        {
            return await _repository.HarvestGRNFarmerRepository.CreateHarvestGRNFarmer(materialDetail);
        }
    }
}