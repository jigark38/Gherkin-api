using GherkinWebAPI.Core;
using GherkinWebAPI.Core.Villages;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VillageDetail = GherkinWebAPI.Models.Villages.VillageDetail;

namespace GherkinWebAPI.Service
{
    public class VillageService : IVillageService
    {
        public IRepositoryWrapper _repository { get; }

        public VillageService(IRepositoryWrapper repository)
        {
            this._repository = repository;
        }
        public async Task<VillageDetail> AddVillage(VillageDetail villageDetail)
        {
            return await _repository.Village.AddVillage(villageDetail);
        }

        public async Task<List<VillageDetail>> GetAllVillagesAsync()
        {
            return await _repository.Village.GetAllVillagesAsync();
        }

        public async Task<VillageDetail> GetVillageByCodeAsync(int villageCode)
        {
            return await _repository.Village.GetVillageByCodeAsync(villageCode);
        }

        public async Task<List<VillageDetail>> GetVillageByMandalCodeAsync(int mandalCode)
        {
            return await _repository.Village.GetVillageByMandalCodeAsync(mandalCode);
        }

        public async Task<bool> IsVillageExist(int countryCode, int stateCode, int districtCode, int mandalCode, string villageName)
        {
            return await _repository.Village.IsVillageExist(countryCode, stateCode, districtCode, mandalCode, villageName);
        }

        public async Task<VillageDetail> UpdateVillage(VillageDetail villageDetail)
        {
            return await _repository.Village.UpdateVillage(villageDetail);
        }

        public async Task<List<Village>> GetVillageListByAreaAndEmployee(string areaId, string employeeId, string villageName)
        {
            return await _repository.Village.GetVillageListByAreaAndEmployee(areaId, employeeId, villageName);
        }

        public async Task<VillageDetail> GetCountryInfoByVillageName(string villageName)
        {
            return await _repository.Village.GetCountryInfoByVillageName(villageName);
        }
    }
}