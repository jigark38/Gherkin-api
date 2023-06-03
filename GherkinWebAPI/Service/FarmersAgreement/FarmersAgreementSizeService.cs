using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service
{
    public class FarmersAgreementSizeService : IFarmersAgreementSizeService
    {
        public IRepositoryWrapper _repository { get; }

        public FarmersAgreementSizeService(IRepositoryWrapper repository)
        {
            this._repository = repository;
        }

        public async Task<FarmersAgreementSizeDetail> CreateAgreementSize(FarmersAgreementSizeDetail farmersAgreementSize)
        {
            return await _repository.FarmersAgreementSizeRepository.CreateAgreementSize(farmersAgreementSize);
        }

        public async Task<FarmersAgreementSizeDetail> UpdateAgreementSize(string farmersAgreementCode, FarmersAgreementSizeDetail farmersAgreementSize)
        {
            return await _repository.FarmersAgreementSizeRepository.UpdateAgreementSize(farmersAgreementCode, farmersAgreementSize);
        }

        public async Task DeleteAgreementSize(string farmersAgreementCode, string cropSchemeCode)
        {
            await _repository.FarmersAgreementSizeRepository.DeleteAgreementSize(farmersAgreementCode, cropSchemeCode);
        }
    }
}