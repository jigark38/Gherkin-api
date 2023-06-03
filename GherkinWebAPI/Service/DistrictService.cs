using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DistrictDetail = GherkinWebAPI.Models.Districts.DistrictDetail;

namespace GherkinWebAPI.Service
{
    public class DistrictService : IDistrictService
    {
        public IDistrictRepository _repository { get; }

        public DistrictService(IDistrictRepository repository)
        {
            this._repository = repository;
        }

        public Task<List<District>> GetAllDistrictsByStateAsync(int stateCode)
        {
            return _repository.GetAllDistrictsByStateIdAsync(stateCode);
        }

        public Task<District> GetDistrictByIdAsync(int districtCode)
        {
            return _repository.GetDistrictByIdAsync(districtCode);
        }

        public async Task<DistrictDetail> AddDistrict(DistrictDetail districtDetail)
        {
            return await _repository.AddDistrict(districtDetail);
        }
    }
}