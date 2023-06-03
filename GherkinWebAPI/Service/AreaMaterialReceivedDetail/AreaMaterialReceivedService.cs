using GherkinWebAPI.Core;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service
{
    public class AreaMaterialReceivedService : IAreaMaterialReceivedService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public AreaMaterialReceivedService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<AreaMaterialReceivedDetailsModel> CreateAreaDetails(AreaMaterialReceivedDetailsModel AreaMaterialReceivedDetailsModel)
        {
            return await _repositoryWrapper.AreaMaterialReceivedRepository.CreateAreaDetails(AreaMaterialReceivedDetailsModel);
        }

        public IEnumerable<AreaMaterialReceivedDetailsModel> AllAsync()
        {
            return  _repositoryWrapper.AreaMaterialReceivedRepository.GetAllAsync();
        }
        public List<Area> GetHarvestAreaDetails()
        {
            return _repositoryWrapper.AreaMaterialReceivedRepository.GetHarvestAreaDetails();
        }

        public async Task<AreaMaterialReceivedDetailsModel> UpdateAreaDetails(int id, AreaMaterialReceivedDetailsModel areaMaterialReceivedDetailsModel)
        {
            return await _repositoryWrapper.AreaMaterialReceivedRepository.UpdateAreaDetails(id, areaMaterialReceivedDetailsModel);
        }
    }
}