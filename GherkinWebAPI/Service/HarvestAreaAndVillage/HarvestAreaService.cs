using GherkinWebAPI.Core;
using GherkinWebAPI.Core.Harvest_Area;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.HarvestAreaAndVillage
{
    public class HarvestAreaService : IHarvestAreaService
    {
        public IRepositoryWrapper _repository { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public HarvestAreaService(IRepositoryWrapper repository)
        {
            this._repository = repository;
        }

        public Task<List<HarvestArea>> SearchArea(string areaId)
        {
            return _repository.HarvestArea.SearchArea(areaId);
        }

        public async Task<Area> AddArea(Area area)
        {
            return await _repository.HarvestArea.AddArea(area);
        }

        public Task<List<Area>> GetAreaNameAndCodeAsync()
        {
            return _repository.HarvestArea.GetAreaNameAndCodeAsync();
        }

        public async Task<bool> IsAreaCodeAllowed(int areaCode)
        {
            return await _repository.HarvestArea.IsAreaCodeAllowed(areaCode);
        }

        public async Task<bool> IsAreaNameAllowed(string areaName)
        {
            return await _repository.HarvestArea.IsAreaNameAllowed(areaName);
        }

        public async Task<List<HarvestArea>> SearchAreaByStateCode(int stateId)
        {
          return  await _repository.HarvestArea.SearchAreaByStateCode(stateId);
        }
    }
}