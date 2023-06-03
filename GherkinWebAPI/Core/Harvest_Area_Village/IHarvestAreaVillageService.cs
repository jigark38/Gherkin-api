using GherkinWebAPI.Models;
using GherkinWebAPI.Models.Harvest_Area;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.Harvest_Area_Village
{
    public interface IHarvestAreaVillageService
    {
        Task<HarvestAreaVillageDetail> UpdateHarvestAreaVillage(HarvestAreaVillageDetail harvestAreaVillage);
        Task<HarvestAreaVillageDetail> AddAreaVillage(HarvestAreaVillageDetail harvestAreaVillages);
        Task<bool> IsVillageCodeAllowed(int villageCode);
        Task<bool> IsAreaVillageAllowed(string areaId, int countryCode, int stateCode, int districtCode, int mandalCode, int villageCode);
        Task<List<HarvestArea>> GetAvailableVillagesAsync(string areaId, string areaName, int countryCode, int stateCode, int districtCode, int mandalCode);
    }
}
