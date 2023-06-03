using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GherkinWebAPI.Core;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.Employee;

namespace GherkinWebAPI.Service
{
    public class ESICService : IESICService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public ESICService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<ESICRate> CreateESICRates(ESICRate esicRate)
        {
            return await _repositoryWrapper.ESICRepository.CreateESICRates(esicRate);
        }

        public async Task<List<ESICRate>> GetESICRates()
        {
            return await _repositoryWrapper.ESICRepository.GetESICRates();
        }

        public async Task<ESICRate> UpdateESICRates(ESICRate esicRate)
        {
            return await _repositoryWrapper.ESICRepository.UpdateESICRates(esicRate);
        }
    }
}