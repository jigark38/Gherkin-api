using GherkinWebAPI.Core;
using GherkinWebAPI.Core.Harvest_Area_Village;
using System.Collections.Generic;
using System.Threading.Tasks;
using GherkinWebAPI.Models.Harvest_Area;
using GherkinWebAPI.Models;

namespace GherkinWebAPI.Service.HarvestAreaAndVillage
{
    public class HarvestAreaVillageService : IHarvestAreaVillageService
    {
        public IRepositoryWrapper _repository { get; }

        public HarvestAreaVillageService(IRepositoryWrapper repository)
        {
            this._repository = repository;
        }

        public async Task<HarvestAreaVillageDetail> AddAreaVillage(HarvestAreaVillageDetail harvestAreaVillage)
        {
            return await _repository.HarvestAreaVillage.AddAreaVillage(harvestAreaVillage);
        }

        public async Task<HarvestAreaVillageDetail> UpdateHarvestAreaVillage(HarvestAreaVillageDetail harvestAreaVillage)
        {
            return await _repository.HarvestAreaVillage.UpdateHarvestAreaVillage(harvestAreaVillage);
        }

        public async Task<bool> IsVillageCodeAllowed(int villageCode)
        {
            return await _repository.HarvestAreaVillage.IsVillageCodeAllowed(villageCode);
        }

        public async Task<bool> IsAreaVillageAllowed(string areaId, int countryCode, int stateCode, int districtCode, int mandalCode, int villageCode)
        {
            return await _repository.HarvestAreaVillage.IsAreaVillageAllowed(areaId, countryCode, stateCode, districtCode, mandalCode, villageCode);
        }

        public async Task<List<HarvestArea>> GetAvailableVillagesAsync(string areaId, string areaName, int countryCode, int stateCode, int districtCode, int mandalCode)
        {
            return await _repository.HarvestAreaVillage.GetAvailableVillagesAsync(areaId, areaName, countryCode, stateCode, districtCode, mandalCode);
        }
    }
}