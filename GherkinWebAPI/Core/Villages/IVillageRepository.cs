using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GherkinWebAPI.DTO.DailyInputAndFeedingDetails;
using GherkinWebAPI.Models;
using VillageDetail = GherkinWebAPI.Models.Villages.VillageDetail;

namespace GherkinWebAPI.Core.Villages
{
    public interface IVillageRepository
    {
        Task<List<VillageDetail>> GetAllVillagesAsync();
        Task<VillageDetail> GetVillageByCodeAsync(int villageCode);
        Task<VillageDetail> AddVillage(VillageDetail villageDetail);
        Task<List<VillageDetail>> GetVillageByMandalCodeAsync(int mandalCode);
        Task<bool> IsVillageExist(int countryCode, int stateCode, int districtCode, int mandalCode, string villageName);
        Task<VillageDetail> UpdateVillage(VillageDetail villageDetail);
        Task<List<Village>> GetVillageListByAreaAndEmployee(string areaId, string employeeId, string villageName);
        Task<VillageDetail> GetCountryInfoByVillageName(string villageName);
    }
}
