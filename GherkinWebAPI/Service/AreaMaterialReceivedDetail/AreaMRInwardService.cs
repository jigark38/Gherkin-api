using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service
{
    public class AreaMRInwardService : IAreaMRInwardService
    {

        private readonly IRepositoryWrapper _repositoryWrapper;
        public AreaMRInwardService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<bool> CreateAreaDetails(AreaMRInwardPostDetails areaMRInwardDetails)
        {
            return await _repositoryWrapper.AreaMRInwardRepository.CreateAreaDetails(areaMRInwardDetails);
        }

        public IEnumerable<AreaMRInwardDetails> GetAllAsync()
        {
            return  _repositoryWrapper.AreaMRInwardRepository.GetAllAsync();
        }

        public async Task<IEnumerable<ARMaterialOutwadDetailsDTO>> GetAMRbyAreaId(string areadId)
        {
            return await _repositoryWrapper.AreaMRInwardRepository.GetMaterialOutwadDetailsByAreaId(areadId);
        }

        public IEnumerable<ARMaterialGridUnderDTO> Getnote2(string areaId, string RMTransferNo)
        {
            return _repositoryWrapper.AreaMRInwardRepository.Getnote2(areaId, RMTransferNo);
        }

        public IEnumerable<NOTE1> Getnote1(string areaId, string RMTransferNo)
        {
            return _repositoryWrapper.AreaMRInwardRepository.Getnote1(areaId, RMTransferNo);
        }

        public async Task<AreaMRInwardDetails> UpdateAreaDetails(int id, AreaMRInwardDetails areaMRInwardDetails)
        {
            return await _repositoryWrapper.AreaMRInwardRepository.UpdateAreaDetails(id, areaMRInwardDetails);
        }

        public long GetAreaMRNo()
        {
            return _repositoryWrapper.AreaMRInwardRepository.GetAreaMRNo();
        }
    }
}