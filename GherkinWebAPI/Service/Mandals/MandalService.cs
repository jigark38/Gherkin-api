using GherkinWebAPI.Core;
using GherkinWebAPI.Core.Mandals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GherkinWebAPI.Models.Mandals;

namespace GherkinWebAPI.Service.Mandals
{
    public class MandalService : IMandalService
    {
        private readonly IRepositoryWrapper _repository;
        public MandalService(IRepositoryWrapper repository)
        {
            _repository = repository;
        }
        public async Task<MandalDetail> AddMandal(MandalDetail mandal)
        {
            return await _repository.MandalRepository.AddMandal(mandal);
        }

        public async Task<List<MandalDetail>> GetAllMandalsAysnc()
        {
            return await _repository.MandalRepository.GetAllMandalsAysnc();
        }

        public async Task<List<MandalDetail>> GetMandalByDistrictCodeAsync(int districtCode)
        {
            return await _repository.MandalRepository.GetMandalByDistrictCodeAsync(districtCode);
        }
    }
}