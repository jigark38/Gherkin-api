using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Service
{
    public class AreaService : IAreaService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public AreaService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public Task AddAreaCode()
        {
            throw new NotImplementedException();
        }

        
        public Task GetAreaCode()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Area>> GetAllArea()
        {
            return await _repositoryWrapper.AreaRepository.GetAllArea();
        }

        public async Task CreateArea(Area area)
        {
            area.Area_Entry_Date = DateTime.UtcNow;
            _repositoryWrapper.AreaRepository.CreateArea(area);
            await _repositoryWrapper.SaveAsync(); 
            //throw new NotImplementedException();
        }

        //Task<List<Area>> IAreaService.GetAllArea()
        //{
        //    throw new NotImplementedException();
        //}

        //Task IAreaService.GetAreaCode()
        //{
        //    throw new NotImplementedException();
        //}
    }
}